using RiverPuzzle1.Models;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.Models
{
    public class GooseTests
    {
        [Fact]
        public void NotCompatibleCharacters_ReturnsListOfStrings()
        {
            // Arrange
            var character = new Goose();

            // Act
            var result = character.NotCompatibleCharacters;

            // Assert
            Assert.IsType<Dictionary<string,string>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Fox will eat the goose", result["Fox"]);
            Assert.Equal("Goose will eat the bean", result["Bean"]);
        }
    }
}
