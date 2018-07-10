using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Moq;
using RiverPuzzle1.Controllers;
using RiverPuzzle1.Models;
using RiverPuzzle1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RiverPuzzle1.Tests.Controllers
{
    public class GamesControllerTests
    {
        [Fact]
        public void GetGames_ReturnsListOfGames()
        {
            // Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(s => s.GetGames()).Returns(new List<Game>());
            var contoller = new GamesController(gameService.Object);

            // Act 
            var result = contoller.GetGames();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Game>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetGame_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            var gameService = new Mock<IGameService>();
            var contoller = new GamesController(gameService.Object);
            contoller.ModelState.AddModelError("test", "test");

            // Act
            var result = contoller.GetGame(1).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetGame_WhenModelStateIsValidAndGameDoesNotExist_ReturnNotFound()
        {
            // Arrange
            var game = new Game
            {
                ID = 1,
                StartTime = new DateTime(2018, 01, 01, 00, 00, 00)
            };

            var gameService = new Mock<IGameService>();
            gameService.Setup(s => s.GetGame(1)).Returns(Task.FromResult<Game>(null));
            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.GetGame(1);

            // Assert
            var apiResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetGame_WhenModelStateIsValidAndGameExists_ReturnGame()
        {
            // Arrange
            var game = new Game
            {
                ID = 1,
                StartTime = new DateTime(2018, 01, 01, 00, 00, 00)
            };

            var gameService = new Mock<IGameService>();
            gameService.Setup(s => s.GetGame(1)).ReturnsAsync(game);
            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.GetGame(1);

            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            var returnedGame = (Game)apiResult.Value;
            Assert.Equal(1, returnedGame.ID);
        }

        [Fact]
        public async Task PostGame_WhenCalled_ReturnsNewGame()
        {
            // Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(s => s.CreateAsync()).ReturnsAsync(new Game { ID = 1 });
            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.PostGame();

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<Game>(createdResult.Value);
        }

        [Fact]
        public async Task PutGame_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            var gameService = new Mock<IGameService>();
            var controller = new GamesController(gameService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = await controller.PutGame(1, new Game());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutGame_WhenModelStateIsValidAndNoExceptionISThrown_ReturnsOk()
        {
            // Arrange
            var game = new Game { ID = 1 };
            var gameService = new Mock<IGameService>();
            gameService.Setup(
                s => s.UpdateAsync(It.Is<Game>(k => k.ID == 1))
                ).ReturnsAsync(game);

            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.PutGame(1, game);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Game>(okObjectResult.Value);
        }

        [Fact]
        public async Task PutGame_WhenModelStateIsValidAndServiceReturnsNull_ReturnsNoBadRequest()
        {
            // Arrange
            var game = new Game { ID = 1 };
            var gameService = new Mock<IGameService>();
            gameService.Setup(
                s => s.UpdateAsync(It.Is<Game>(k => k.ID == 1))
                ).Returns(Task.FromResult<Game>(null));

            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.PutGame(1, game);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutGame_WhenModelStateIsValidAndInvalidOperationIsRaised_ReturnsBadRequest()
        {
            // Arrange
            var game = new Game { ID = 1 };
            var gameService = new Mock<IGameService>();
            gameService.Setup(
                s => s.UpdateAsync(It.Is<Game>(k => k.ID == 1))
                ).Throws(new InvalidOperationException("invalid"));

            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.PutGame(1, game);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("invalid", badRequestObjectResult.Value);

        }

        [Fact]
        public async Task PutGame_WhenModelStateIsValidAndDbUpdateConcurrencyExceptionIsRaised_ReturnsInternalServerError()
        {
            // Arrange
            var updateEntry = new Mock<IUpdateEntry>();
            IReadOnlyList<IUpdateEntry> entries = new List<IUpdateEntry>
            {
                updateEntry.Object
            };
            var game = new Game { ID = 1 };
            var gameService = new Mock<IGameService>();
            gameService.Setup(
                s => s.UpdateAsync(It.Is<Game>(k => k.ID == 1))
                ).Throws(new DbUpdateConcurrencyException("invalid", entries));

            var controller = new GamesController(gameService.Object);

            // Act
            var result = await controller.PutGame(1, game);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

    }
}
