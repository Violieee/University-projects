using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lab1.lab2;

namespace lab2_test
{
    [TestClass]
    public sealed class SumEvenTests
    {
        [TestMethod]
        public void CalculateSum_From1To10_Returns30()
        {
            // Arrange
            Sum_even sum = new Sum_even(1, 10);

            // Act
            int result = sum.CalculateSum();

            // Assert
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void CalculateSum_From2To6_Returns12()
        {
            // Arrange
            Sum_even sum = new Sum_even(2, 6);

            // Act
            int result = sum.CalculateSum();

            // Assert
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void CalculateSum_WhenNoEvenNumbers_ReturnsZero()
        {
            // Arrange
            Sum_even sum = new Sum_even(3, 3);

            // Act
            int result = sum.CalculateSum();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateSum_WithNegativeNumbers_ReturnsMinus6()
        {
            // Arrange
            Sum_even sum = new Sum_even(-4, 1);

            // Act
            int result = sum.CalculateSum();

            // Assert
            Assert.AreEqual(-6, result);
        }
    }
}