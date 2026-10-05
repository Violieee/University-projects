using lab4.RealEstateRentalApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab4.Tests;

[TestClass]
public class RentalTests
{
    [TestMethod]
    public void CalculateAverageMonthlyPrice_ReturnsCorrectAverage()
    {
        List<RealEstateRental> rentals = new()
        {
            new Apartment("Хрещатик, 10", 20000m, 50, 2),
            new Apartment("Сікорського, 5", 18000m, 45, 1),
            new House("Садова, 15", 34000m, 100, true)
        };

        RentalCalculator calculator = new(rentals);

        decimal result = calculator.CalculateAverageMonthlyPrice();

        Assert.AreEqual(24000m, result);
    }

    [TestMethod]
    public void CalculateAverageMonthlyPrice_EmptyList_ReturnsZero()
    {
        RentalCalculator calculator = new(new List<RealEstateRental>());

        decimal result = calculator.CalculateAverageMonthlyPrice();

        Assert.AreEqual(0m, result);
    }

    [TestMethod]
    public void CalculateAverageMonthlyPrice_UsesMinimumArea()
    {
        List<RealEstateRental> rentals = new()
        {
            new Apartment("A", 10000m, 30, 1),
            new House("B", 30000m, 90, true),
            new Office("C", 50000m, 120)
        };

        RentalCalculator calculator = new(rentals, areaMin: 80);

        decimal result = calculator.CalculateAverageMonthlyPrice();

        Assert.AreEqual(40000m, result);
    }

    [TestMethod]
    public void Apartment_ImplementsUtilityInterface_AndCalculatesUtilities()
    {
        IUtilityCalculable apartment = new Apartment("Хрещатик, 10", 20000m, 50, 2);

        decimal result = apartment.CalculateUtilities();

        Assert.AreEqual(2000m, result);
    }

    [TestMethod]
    public void House_ImplementsInfrastructureInterface_AndEvaluatesInfrastructure()
    {
        IInfrastructureEvaluable house = new House("Садова, 15", 30000m, 100, true)
        {
            PublicTransportNearby = true,
            ShopNearby = true,
            SchoolNearby = false
        };

        string result = house.EvaluateInfrastructure();

        Assert.AreEqual("Інфраструктура розвинена задовільно.", result);
    }

    [TestMethod]
    public void Office_IsAdditionalClass_ThatImplementsUtilityInterface()
    {
        IUtilityCalculable office = new Office("Ділова, 1", 45000m, 100);

        decimal result = office.CalculateUtilities();

        Assert.AreEqual(5500m, result);
    }

    [TestMethod]
    public void UtilityInterface_Polymorphism_WorksWithDifferentClasses()
    {
        List<IUtilityCalculable> utilityObjects = new()
        {
            new Apartment("Квартира", 15000m, 50, 2),
            new Office("Офіс", 40000m, 100)
        };

        decimal totalUtilities = utilityObjects.Sum(item => item.CalculateUtilities());

        Assert.AreEqual(7500m, totalUtilities);
    }

    [TestMethod]
    public void RentableInterface_Polymorphism_AcceptsDifferentRentalTypes()
    {
        List<IRentable> rentableObjects = new()
        {
            new Apartment("Квартира", 15000m, 50, 2),
            new House("Будинок", 30000m, 100, true),
            new Office("Офіс", 40000m, 80)
        };

        foreach (IRentable rentable in rentableObjects)
        {
            rentable.Pay();
            rentable.TerminateContract();
        }

        Assert.AreEqual(3, rentableObjects.Count);
    }
}
