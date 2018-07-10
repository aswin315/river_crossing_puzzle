using Microsoft.EntityFrameworkCore;
using Moq;
using RiverPuzzle.Services;
using RiverPuzzle1.Data;
using RiverPuzzle1.Models;
using RiverPuzzle1.Services;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.Services
{
    public class GameStateServiceTests
    {
        private readonly DbContextOptions<GameContext> _contextOptions;

        public GameStateServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<GameContext>()
             .UseInMemoryDatabase(databaseName: "RiverPuzzleTest")
             .Options;
        }

        [Fact]
        public void Build_ReturnsNewGameState()
        {
            // Arrange
            var context = new GameContext(_contextOptions);
            var characterService = new Mock<ICharacterService>();

            var service = new GameStateService(context, characterService.Object);
            
            // Act
            var gameState = service.Build();

            // Assert
            Assert.Equal(State.InProgress, gameState.State);

            var characters = gameState.Characters;
            Assert.Equal(4, characters.Count);
            Assert.Equal(Location.LeftBank, characters[0].Location);
        }

        [Fact]
        public void Update_WhenCalledWithNewGameState_UpdatesExistingGameState()
        {
            // Arrange
            SetupGameStates(
                out GameState existingGameState,
                out GameState newGameState,
                Location.RightBank,
                Location.LeftBank);
            var context = new GameContext(_contextOptions);
            var characterService = new Mock<ICharacterService>();
            characterService.Setup(s => s.Validate(newGameState.Characters)).Returns(string.Empty);

            var service = new GameStateService(context, characterService.Object);

            // Act
            service.Update(newGameState, existingGameState);

            // Assert
            var farmerLocation = existingGameState.Characters.Find(r => r.ID == 1).Location;
            Assert.Equal(Location.RightBank, farmerLocation);
        }

        [Fact]
        public void Update_WhenCharacterListsIsNOtValid_UpdatesGameStateToFailWithReason()
        {
            // Arrange
            SetupGameStates(
                out GameState existingGameState,
                out GameState newGameState,
                Location.RightBank,
                Location.RightBank);

            var context = new GameContext(_contextOptions);
            var characterService = new Mock<ICharacterService>();
            characterService.Setup(s => s.Validate(newGameState.Characters)).Returns("failed");
            var service = new GameStateService(context, characterService.Object);

            // Act
            service.Update(newGameState, existingGameState);

            // Assert
            var farmerLocation = existingGameState.Characters.Find(r => r.ID == 1).Location;
            Assert.Equal(Location.LeftBank, farmerLocation);
            Assert.Equal(State.Fail, existingGameState.State);
            Assert.Equal("failed", existingGameState.Reason);
        }
        [Fact]
        public void Update_WhenEveryCharacterIsInRightBank_UpdatesGameStateToSuccess()
        {
            // Arrange
            SetupGameStates(
                out GameState existingGameState,
                out GameState newGameState,
                Location.RightBank,
                Location.RightBank);

            var context = new GameContext(_contextOptions);
            var characterService = new Mock<ICharacterService>();
            characterService.Setup(s => s.Validate(newGameState.Characters)).Returns(string.Empty);
            var service = new GameStateService(context, characterService.Object);
            
            // Act
            service.Update(newGameState, existingGameState);

            // Assert
            var farmerLocation = existingGameState.Characters.Find(r => r.ID == 1).Location;
            Assert.Equal(Location.RightBank, farmerLocation);
            Assert.Equal(State.Pass, existingGameState.State);
        }

        private static void SetupGameStates(
          out GameState existingGameState,
          out GameState newGameState,
          Location farmerLocation,
          Location foxLocation)
        {
            existingGameState = new GameState
            {
                Characters = new List<Character>
                {
                    new Farmer
                    {
                        ID = 1,
                        Location = Location.LeftBank
                    },
                    new Fox
                    {
                        ID = 2,
                        Location = Location.LeftBank
                    }
                }
            };
            newGameState = new GameState
            {
                Characters = new List<Character>
                {
                    new Farmer
                    {
                        ID = 1,
                        Location = farmerLocation
                    },
                    new Fox
                    {
                        ID = 2,
                        Location = foxLocation
                    }
                }
            };
        }

    }
}
