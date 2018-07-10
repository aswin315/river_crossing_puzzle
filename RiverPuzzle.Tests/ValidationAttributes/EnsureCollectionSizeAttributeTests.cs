using RiverPuzzle1.ValidationAttributes;
using System.Collections.Generic;
using Xunit;

namespace RiverPuzzle1.Tests.ValidationAttributes
{
    public class EnsureCollectionSizeAttributeTests
    {
        [Fact]
        public void IsValid_WhenCollectionisEmptyAndExpectedSizeis2_ReturnsFalse()
        {
            // Arrange
            var attribute = new EnsureCollectionSizeAttribute(2);

            // Act
            var result = attribute.IsValid(new List<string>());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_WhenCollectionSizeIsEmptyAndExpectedSizeis0_ReturnsFalse()
        {
            // Arrange
            var attribute = new EnsureCollectionSizeAttribute(0);

            // Act
            var result = attribute.IsValid(new List<string>());

            //Assert
            Assert.True(result);
        }
    }
}
