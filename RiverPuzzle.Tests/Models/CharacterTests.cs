using RiverPuzzle1.Models;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.Models
{
    public class CharacterTests
    {
        [Fact]
        public void NotCompatibleCharacters_ReturnsEmptyList()
        {
            // Arrange
            var character = new Character();

            // Act
            var result = character.NotCompatibleCharacters;

            // Assert
            Assert.IsType<Dictionary<string,string>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void CanCoexist_WhenCharactersAreFarmerAndGoose_ReturnsEmpty()
        {
            // Arrange
            var goose = new Goose();
            var farmer = new Farmer();

            // Act
            var result = farmer.CanCoexist(goose);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CanCoexist_WhenCharacterAreFoxAndGoose_ReturnsReason()
        {
            // Arrange
            var goose = new Goose();
            var fox = new Fox();

            // Act
            var result = fox.CanCoexist(goose);

            // Assert
            Assert.Equal("Fox will eat the goose",result);
        }
    }
}
