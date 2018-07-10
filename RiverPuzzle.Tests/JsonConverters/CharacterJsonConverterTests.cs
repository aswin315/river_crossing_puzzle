using Newtonsoft.Json;
using RiverPuzzle1.JsonConverters;
using RiverPuzzle1.Models;
using Xunit;

namespace RiverPuzzle1.Tests.JsonConverters
{
    public class CharacterJsonConverterTests
    {
        [Fact]
        public void CanWrite_ReturnFalse()
        {
            // Arrange
            var converter = new CharacterJsonConverter();

            // Act
            var result = converter.CanWrite;
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CanConvert_WhenTypeIsCharacter_ReturnTrue()
        {
            // Arrange
            var converter = new CharacterJsonConverter();
            var character = new Character().GetType();

            // Act
            var result = converter.CanConvert(character);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanConvert_WhenTypeIsNotCharacter_ReturnFalse()
        {
            // Arrange
            var converter = new CharacterJsonConverter();
            var game = new Game().GetType();

            // Act
            var result = converter.CanConvert(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ReadJson_ConvertsToTheCorrectType()
        {
            // Arrange
            var converter = new CharacterJsonConverter();
            var character = new Character
            {
                ID = 1,
                Name = "Farmer",
                Location = Location.Boat
            };
            var characterJson = JsonConvert.SerializeObject(character);
           
            // Act
            var deserializedObject = JsonConvert.DeserializeObject(characterJson, character.GetType(), new CharacterJsonConverter());

            // Assert
            Assert.IsType<Farmer>(deserializedObject);
        }
    }
}
