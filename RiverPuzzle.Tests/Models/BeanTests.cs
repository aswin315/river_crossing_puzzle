using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = character.NotCompatibleCharacters();

            // Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Goose", result[0]);
        }
    }
}
