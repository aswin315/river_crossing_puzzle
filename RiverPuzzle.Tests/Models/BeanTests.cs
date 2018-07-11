using RiverPuzzle1.Models;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.Models
{
    public class BeanTests
    {
        [Fact]
        public void NotCompatibleCharacters_ReturnsListOfStrings()
        {
            // Arrange
            var character = new Bean();

            // Act
            var result = character.NotCompatibleCharacters;

            // Assert
            Assert.IsType<Dictionary<string, string>>(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Goose will eat the bean", result["Goose"]);
        }
    }
}
