using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab3.RealEstateRentalApp;

namespace lab3.Tests;

[TestClass]
public class RentalCalculatorTests
{
    [TestMethod]
    public void CalculateAveragePricePerSquareMeter_ReturnsCorrectAverage()
    {
        List<RealEstateRental> rentals = new()
        {
            new Apartment("Хрещатик, 10", 20000, 50, 2),
            new Apartment("Сікорського, 5", 18000, 45, 1),
            new House("Садова, 15", 35000, 100, true)
        };

        RentalCalculator calculator = new(rentals);

        decimal result = calculator.CalculateAveragePricePerSquareMeter();
        decimal expected = 383.33m;

        Assert.AreEqual(expected, Math.Round(result, 2));
    }

    [TestMethod]
    public void CalculateAveragePricePerSquareMeter_EmptyList_ReturnsZero()
    {
        List<RealEstateRental> rentals = new();
        RentalCalculator calculator = new(rentals);

        decimal result = calculator.CalculateAveragePricePerSquareMeter();

        Assert.AreEqual(0m, result);
    }

    [TestMethod]
    public void CalculateAveragePricePerSquareMeter_OneObject_ReturnsCorrectValue()
    {
        List<RealEstateRental> rentals = new()
        {
            new Apartment("Хрещатик, 10", 20000, 50, 2)
        };

        RentalCalculator calculator = new(rentals);

        decimal result = calculator.CalculateAveragePricePerSquareMeter();

        Assert.AreEqual(400m, result);
    }

    [TestMethod]
    public void CalculateAveragePricePerSquareMeter_TwoObjects_ReturnsCorrectAverage()
    {
        List<RealEstateRental> rentals = new()
        {
            new Apartment("Хрещатик, 10", 20000, 50, 2),
            new House("Садова, 15", 30000, 100, true)
        };

        RentalCalculator calculator = new(rentals);

        decimal result = calculator.CalculateAveragePricePerSquareMeter();

        Assert.AreEqual(350m, result);
    }

    [TestMethod]
    public void EvaluateInfrastructure_AllObjectsNearby_ReturnsGoodRating()
    {
        House house = new("Садова, 15", 30000, 100, true);

        string result = house.EvaluateInfrastructure(true, true, true);

        Assert.AreEqual("Інфраструктура розвинена добре.", result);
    }

    [TestMethod]
    public void EvaluateInfrastructure_TwoObjectsNearby_ReturnsSatisfactoryRating()
    {
        House house = new("Садова, 15", 30000, 100, true);

        string result = house.EvaluateInfrastructure(true, true, false);

        Assert.AreEqual("Інфраструктура розвинена задовільно.", result);
    }

    [TestMethod]
    public void EvaluateInfrastructure_NoObjectsNearby_ReturnsNoInfrastructureRating()
    {
        House house = new("Садова, 15", 30000, 100, true);

        string result = house.EvaluateInfrastructure(false, false, false);

        Assert.AreEqual("Інфраструктура практично відсутня.", result);
    }
}
