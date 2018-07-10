using Microsoft.EntityFrameworkCore;
using Moq;
using RiverPuzzle1.Data;
using RiverPuzzle1.Models;
using RiverPuzzle1.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.Services
{
    public class GameServiceTests
    {
        private readonly DbContextOptions<GameContext> _contextOptions;

        public GameServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "RiverPuzzleTest")
                .Options;
        }

        [Fact]
        public void CreateAsync_WhenCalled_CreatesNewGameWithCharactersAndReturnsGame()
        {
            var gameStateService = new Mock<IGameStateService>();
            gameStateService.Setup(s => s.Build()).Returns(new GameState());

            using (var context = new GameContext(_contextOptions))
            {
                var service = new GameService(context, gameStateService.Object);
                var game = service.CreateAsync().Result;

                Assert.IsType<Game>(game);
                gameStateService.Verify(s => s.Build(), Times.Once);
            }
        }

        [Fact]
        public void GetGames_WhenGamesTableIsEmpty_ReturnsEmptyListOfGames()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "RiverPuzzleTest1")
                .Options;
            var gameContext = new GameContext(contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);
            // Act
            var result = service.GetGames();
            // Assert
            Assert.IsAssignableFrom<IList<Game>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetGames_WhenGamesExist_ReturnsListOfGamesWithGameState()
        {
            // Arrange
            SetUpGame();
            var gameContext = new GameContext(_contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);
            // Act
            var result = service.GetGames();
            // Assert
            Assert.IsAssignableFrom<IList<Game>>(result);
            Assert.NotEmpty(result);
            Assert.NotNull(result[0].GameState);
        }

        [Fact]
        public void GetGame_WhenGameDoesNotExist_ReturnsNull()
        {
            // Arrange
            var gameContext = new GameContext(_contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);
            // Act
            var result = service.GetGame(100).Result;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetGame_WhenGameExist_ReturnsGameWithGameStateAndCharacters()
        {
            // Arrange
            var game = SetUpGame();
            var gameContext = new GameContext(_contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);
            // Act
            var result = service.GetGame(game.ID).Result;

            // Assert
            Assert.NotNull(result.GameState);
            Assert.NotEmpty(result.GameState.Characters);
        }

        [Fact]
        public async void UpdateGameAsync_WhenGameDoesNotExist_ReturnNull()
        {
            // Arrange
            var gameContext = new GameContext(_contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);

            // Act
            var result = await service.UpdateAsync(new Game { ID = 100 });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateGameAsync_WhenGameExistsAndGameStateIsInValid_ThrowsInvalidOperationException()
        {
            // Arrange
            var game = SetUpGame();
            game.GameState.ID = 1000;
            var gameContext = new GameContext(_contextOptions);
            var gameStateService = new Mock<IGameStateService>();

            var service = new GameService(gameContext, gameStateService.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.UpdateAsync(game));
        }

        [Fact]
        public async void UpdateGameAsync_WhenGameExistsAndGameStateIsValid_UpdatesGame()
        {
            // Arrange
            var game = SetUpGame();
            var gameStateService = new Mock<IGameStateService>();

            gameStateService.Setup(s => s.Update(It.IsAny<GameState>(), It.IsAny<GameState>()));
            game.GameState.Characters[0].Location = Location.Boat;
            var gameContext = new GameContext(_contextOptions);
            var service = new GameService(gameContext, gameStateService.Object);

            // Act
            var updatedGame = await service.UpdateAsync(game);
            // Assert
            Assert.IsType<Game>(updatedGame);
        }

        [Fact]
        public async void UpdateGameAsync_WhenGameExistsAndGameStateIsNotProgree_UpdatesGame()
        {
            // Arrange
            var game = SetUpGame();
            var gameStateService = new Mock<IGameStateService>();

            gameStateService.Setup(s => s.Update(It.IsAny<GameState>(), It.IsAny<GameState>()))
                .Callback((GameState gameState, GameState existingGameState) => { existingGameState.State = State.Pass; });
            game.GameState.State = State.Pass;
            var gameContext = new GameContext(_contextOptions);
            var service = new GameService(gameContext, gameStateService.Object);
            
            // Act
            var updatedGame = await service.UpdateAsync(game);
            
            // Assert
            Assert.IsType<Game>(updatedGame);
            Assert.Equal(State.Pass, updatedGame.GameState.State);
            Assert.NotEqual(DateTime.MinValue, updatedGame.EndTime);            
        }

        private Game SetUpGame()
        {
            var game = new Game
            {
                StartTime = DateTime.Now,
                GameState = new GameState
                {
                    State = State.InProgress,
                    Characters = new List<Character>
                        {
                            new Farmer{Location = Location.LeftBank },
                            new Fox{Location = Location.LeftBank },
                            new Goose{Location = Location.LeftBank},
                            new Bean{Location = Location.LeftBank}
                        }
                }
            };
            using (var context = new GameContext(_contextOptions))
            {
                context.Add(game);
                context.SaveChanges();
            }
            return game;
        }
    }
}
