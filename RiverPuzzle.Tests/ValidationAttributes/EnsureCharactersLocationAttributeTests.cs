using RiverPuzzle1.Models;
using RiverPuzzle1.ValidationAttributes;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.ValidationAttributes
{
    public class EnsureCharactersLocationAttributeTests
    {
        [Fact]
        public void IsValid_WhenOnlyCharacterExistsInTheList_ReturnsTrue()
        {
            // Arrange
            var attribute = new EnsureCharactersLocationAttribute();
            var characters = new List<Character>
            {
                new Farmer
                {
                    Location = Location.LeftBank
                }
            };

            // Act
            var result = attribute.IsValid(characters);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_WhenAllCharactersExistsInTheSameLocation_ReturnsTrue()
        {
            // Arrange
            var attribute = new EnsureCharactersLocationAttribute();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.LeftBank},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.LeftBank}
            };

            // Act
            var result = attribute.IsValid(characters);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_WhenThreeCharactersExistsInTheSameLocation_ReturnsFalse()
        {
            // Arrange
            var attribute = new EnsureCharactersLocationAttribute();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.LeftBank},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.Boat}
            };

            // Act
            var result = attribute.IsValid(characters);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_WhenFoxAndGooseCharactersExistsInTheSameLocation_ReturnsFalse()
        {
            // Arrange
            var attribute = new EnsureCharactersLocationAttribute();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.Boat},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.Boat}
            };

            // Act
            var result = attribute.IsValid(characters);

            // Assert
            Assert.False(result);
            Assert.Equal("Fox will eat the goose", attribute.ErrorMessage);
        }

        [Fact]
        public void IsValid_WhenGooseAndBeanCharactersExistsInTheSameLocation_ReturnsFalse()
        {
            // Arrange
            var attribute = new EnsureCharactersLocationAttribute();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.Boat},
                new Fox{Location = Location.Boat},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.LeftBank}
            };

            // Act
            var result = attribute.IsValid(characters);

            // Assert
            Assert.False(result);
            Assert.Equal("Goose will eat the bean", attribute.ErrorMessage);
        }

    }
}
