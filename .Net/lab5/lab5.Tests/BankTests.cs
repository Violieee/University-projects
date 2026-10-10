using lab5.Core.FileStorage;
using lab5.Core.Models;
using lab5.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class BankTests
{
    private static Bank CreateBank() =>
        new(new JsonFileWriter(), new JsonFileReader(),
            Path.Combine(Path.GetTempPath(), $"lab5-bank-test-{Guid.NewGuid():N}.json"));

    [TestMethod]
    public void AddDepositor_AddsToCollection()
    {
        var bank = CreateBank();
        bank.AddDepositor(new Depositor("Олена", "UA1", 200m, DateTime.Now.Year));
        Assert.AreEqual(1, bank.Deposits.Count);
        Assert.AreEqual(1, bank.GetAllDepositors().Count);
    }

    [TestMethod]
    public void AddDepositor_DuplicateAccount_Throws()
    {
        var bank = CreateBank();
        bank.AddDepositor(new Depositor("Олена", "UA1", 200m, 2024));
        Assert.ThrowsException<InvalidOperationException>(() =>
            bank.AddDepositor(new Depositor("Іван", "ua1", 500m, 2024)));
    }

    [TestMethod]
    public void AddDepositor_UninitializedDepositor_Throws()
    {
        var bank = CreateBank();
        Assert.ThrowsException<ArgumentException>(() => bank.AddDepositor(new Depositor()));
    }

    [TestMethod]
    public void RemoveDepositor_RemovesFromBothCollections()
    {
        var bank = CreateBank();
        bank.AddDepositor(new Depositor("Олена", "UA1", 200m, DateTime.Now.Year));
        bank.RemoveDepositor("UA1");
        Assert.AreEqual(0, bank.Deposits.Count);
        Assert.AreEqual(0, bank.GetAllDepositors().Count);
    }

    [TestMethod]
    public void GetCurrentYearDepositors_ReturnsCurrentYearOnly()
    {
        var bank = CreateBank();
        bank.AddDepositor(new Depositor("А", "UA1", 100m, DateTime.Now.Year));
        bank.AddDepositor(new Depositor("Б", "UA2", 200m, DateTime.Now.Year - 1));
        var result = bank.GetCurrentYearDepositors();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("UA1", result[0].AccountNumber);
    }

    [TestMethod]
    public void SortByAmount_ReturnsNewSortedList()
    {
        var bank = CreateBank();
        var source = new List<Depositor>
        {
            new("Дорогий", "UA2", 600m, 2024),
            new("Дешевий", "UA1", 100m, 2024)
        };
        var sorted = bank.SortByAmount(source);
        Assert.AreEqual("UA1", sorted[0].AccountNumber);
        Assert.AreEqual("UA2", source[0].AccountNumber);
    }

    [TestMethod]
    public void FilterByYear_ImmediatelyReturnsSortedNewList()
    {
        var bank = CreateBank();
        int year = DateTime.Now.Year;
        bank.AddDepositor(new Depositor("Більше", "UA1", 900m, year));
        bank.AddDepositor(new Depositor("Менше", "UA2", 100m, year));
        bank.AddDepositor(new Depositor("Минулого року", "UA3", 50m, year - 1));

        bank.FilterByYear(year);

        Assert.AreEqual(2, bank.Deposits.Count);
        Assert.AreEqual("UA2", bank.Deposits[0].AccountNumber);
        Assert.AreEqual("UA1", bank.Deposits[1].AccountNumber);
    }

    [TestMethod]
    public void FilterThenSort_UpdatesVisibleButNotAllDepositors()
    {
        var bank = CreateBank();
        int year = DateTime.Now.Year;
        bank.AddDepositor(new Depositor("А", "UA1", 500m, year));
        bank.AddDepositor(new Depositor("Б", "UA2", 100m, year - 1));
        bank.AddDepositor(new Depositor("В", "UA3", 200m, year));
        bank.FilterByYear(year);
        bank.SortVisibleByAmount();

        Assert.AreEqual(2, bank.Deposits.Count);
        Assert.AreEqual("UA3", bank.Deposits[0].AccountNumber);
        Assert.AreEqual("UA1", bank.Deposits[1].AccountNumber);
        Assert.AreEqual(3, bank.GetAllDepositors().Count);

        bank.ResetFilter();
        Assert.AreEqual(3, bank.Deposits.Count);
    }

    [TestMethod]
    public void AddWhileFiltered_OnlyDisplaysMatchingYear()
    {
        var bank = CreateBank();
        int year = DateTime.Now.Year;
        bank.FilterByYear(year);
        bank.AddDepositor(new Depositor("Не той рік", "UA2", 100m, year - 1));
        bank.AddDepositor(new Depositor("Той рік", "UA1", 200m, year));
        Assert.AreEqual(1, bank.Deposits.Count);
        Assert.AreEqual(2, bank.GetAllDepositors().Count);
    }
}
