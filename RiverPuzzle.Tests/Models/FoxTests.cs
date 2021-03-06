﻿using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RiverPuzzle1.Tests.Models
{
    public class FoxTests
    {
        [Fact]
        public void NotCompatibleCharacters_ReturnsListOfStrings()
        {
            // Arrange
            var character = new Fox();

            // Act
            var result = character.NotCompatibleCharacters;

            // Assert
            Assert.IsType<Dictionary<string, string>>(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Fox will eat the goose", result["Goose"]);
        }
    }
}
