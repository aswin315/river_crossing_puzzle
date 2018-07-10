using RiverPuzzle.Factories;
using RiverPuzzle1.Models;
using Xunit;

namespace RiverPuzzle.Tests.Factories
{
    public class CharacterFactoryTests
    {
        [Fact]
        public void Build_WhenCalledWithValidName_ReturnsCorrectCharacter()
        {
            // Arrange
            // Act
            var character = CharacterFactory.Build("Farmer");

            // Assert
            Assert.IsType<Farmer>(character);
        }

        [Fact]
        public void Build_WhenCalledWithInValidName_ReturnsNull()
        {
            // Arrange
            // Act
            var character = CharacterFactory.Build("Invalid");

            // Assert
            Assert.Null(character);
        }
    }
}
