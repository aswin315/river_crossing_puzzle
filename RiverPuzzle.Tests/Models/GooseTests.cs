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
            var result = character.NotCompatibleCharacters();

            // Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Fox", result[0]);
            Assert.Equal("Bean", result[1]);
        }
    }
}
