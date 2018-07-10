using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RiverPuzzle1.Tests.Models
{
    public class GameStateTests
    {
        [Fact]
        public void Equals_WhenObjectIsNull_ReturnsFalse()
        {
            // Arrange
            var gameState = new GameState { ID = 1 };
            // Act
            var result = gameState.Equals(null);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenObjectIsOfDifferentType_ReturnsFalse()
        {
            // Arrange
            var gameState = new GameState { ID = 1 };
            // Act
            var result = gameState.Equals(new Game());
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenIdsAreDifferent_ReturnsFalse()
        {
            // Arrange
            var gameState = new GameState { ID = 1 };
            var gameStateToVerify = new GameState { ID = 2 };

            // Act
            var result = gameState.Equals(gameStateToVerify);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenCharacterIdsAreDifferent_ReturnsFalse()
        {
            // Arrange
            var gameState = new GameState
            {
                ID = 1,
                Characters = new List<Character>()
            };

            var gameStateToVerify = new GameState
            {
                ID = 1,
                Characters = new List<Character>
                {
                    new Character()
                }
            };

            // Act
            var result = gameState.Equals(gameStateToVerify);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenIdAndCharacterIdsAreSame_ReturnsTrue()
        {
            // Arrange
            var gameState = new GameState
            {
                ID = 1,
                Characters = new List<Character>
                {
                    new Fox{ID = 1}
                }
            };

            var gameStateToVerify = new GameState
            {
                ID = 1,
                Characters = new List<Character>
                {
                    new Fox{ID = 1}
                }
            };

            // Act
            var result = gameState.Equals(gameStateToVerify);

            // Assert
            Assert.True(result);
        }
    }
}
