using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lab1.lab2;

namespace lab2_test
{
    [TestClass]
    public sealed class SphereSegmentTests
    {
        [TestMethod]
        public void GetVolume_ReturnsCorrectValue()
        {
            // Arrange
            SphereSegment segment = new SphereSegment(10, 3);

            // Act
            double result = segment.GetVolume();

            // Assert
            double expected = 81 * Math.PI;

            Assert.AreEqual(expected, result, 0.001);
        }

        [TestMethod]
        public void GetBaseRadius_ReturnsCorrectValue()
        {
            // Arrange
            SphereSegment segment = new SphereSegment(10, 3);

            // Act
            double result = segment.GetBaseRadius();

            // Assert
            double expected = Math.Sqrt(51);

            Assert.AreEqual(expected, result, 0.001);
        }

        [TestMethod]
        public void GetCurvedSurfaceArea_ReturnsCorrectValue()
        {
            // Arrange
            SphereSegment segment = new SphereSegment(10, 3);

            // Act
            double result = segment.GetCurvedSurfaceArea();

            // Assert
            double expected = 60 * Math.PI;

            Assert.AreEqual(expected, result, 0.001);
        }
    }
}