using RiverPuzzle.Services;
using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RiverPuzzle.Tests.Services
{
    public class CharacterServiceTests
    {
        [Fact]
        public void Validate_WhenOnlyOneCharacterExistsInTheList_ReturnsEmptyString()
        {
            // Arrange
            var characterService = new CharacterService();
            var characters = new List<Character>
            {
                new Farmer
                {
                    Location = Location.LeftBank
                }
            };

            // Act
            var result = characterService.Validate(characters);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Validate_WhenAllCharactersExistsInTheSameLocation_ReturnsEmptyString()
        {
            // Arrange
            var characterService = new CharacterService();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.LeftBank},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.LeftBank}
            };

            // Act
            var result = characterService.Validate(characters);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Validate_WhenThreeCharactersExistsInTheSameLocation_ReturnsReason()
        {
            // Arrange
            var characterService = new CharacterService();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.LeftBank},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.Boat}
            };

            // Act
            var result = characterService.Validate(characters);

            // Assert
            Assert.Equal("Too many characters in the same location", result);
        }

        [Fact]
        public void Validate_WhenFoxAndGooseCharactersExistsInTheSameLocation_ReturnsReason()
        {
            // Arrange
            var characterService = new CharacterService();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.Boat},
                new Fox{Location = Location.LeftBank},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.Boat}
            };

            // Act
            var result = characterService.Validate(characters);

            // Assert
            Assert.Equal("Fox will eat the goose", result);
        }

        [Fact]
        public void Validate_WhenGooseAndBeanCharactersExistsInTheSameLocation_ReturnsReason()
        {
            // Arrange
            var characterService = new CharacterService();
            var characters = new List<Character>
            {
                new Farmer{Location = Location.Boat},
                new Fox{Location = Location.Boat},
                new Goose{Location = Location.LeftBank},
                new Bean{Location = Location.LeftBank}
            };

            // Act
            var result = characterService.Validate(characters);

            // Assert
            Assert.Equal("Goose will eat the bean", result);
        }
    }
}
