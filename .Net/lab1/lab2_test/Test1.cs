using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lab1.lab2;

namespace lab2_test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Calculate_WhenEvenNumberExists_ReturnsCubeOfSum()
        {
            // Arrange
            Cube_sum cube = new Cube_sum(2, 3, 4);

            // Act
            int result = cube.Calculate();

            // Assert
            Assert.AreEqual(729, result);
        }

        [TestMethod]
        public void Calculate_WhenAllNumbersOdd_ReturnsSumOfCubes()
        {
            // Arrange
            Cube_sum cube = new Cube_sum(1, 3, 5);

            // Act
            int result = cube.Calculate();

            // Assert
            Assert.AreEqual(153, result);
        }

        [TestMethod]
        public void HasEvenNumber_WhenEvenNumberExists_ReturnsTrue()
        {
            // Arrange
            Cube_sum cube = new Cube_sum(1, 2, 3);

            // Act
            bool result = cube.HasEvenNumber();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasEvenNumber_WhenAllNumbersOdd_ReturnsFalse()
        {
            // Arrange
            Cube_sum cube = new Cube_sum(1, 3, 5);

            // Act
            bool result = cube.HasEvenNumber();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
