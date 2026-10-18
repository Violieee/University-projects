using lab5.Core.FileStorage;
using lab5.Core.Models;
using lab5.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class LinqBankTests
{
    private static Bank CreateBankWithDepositors()
    {
        var bank = new Bank(new JsonFileWriter(), new JsonFileReader(),
            Path.Combine(Path.GetTempPath(), $"linq-test-{Guid.NewGuid():N}.json"));
        bank.AddDepositor(new Depositor("Олена", "UA1", 100m, 2023));
        bank.AddDepositor(new Depositor("Іван", "UA2", 300m, 2024));
        bank.AddDepositor(new Depositor("Марія", "UA3", 200m, 2023));
        return bank;
    }

    [TestMethod]
    public void Projection_Names_ReturnsAllNamesInInsertionOrder()
    {
        CollectionAssert.AreEqual(new[] { "Олена", "Іван", "Марія" },
            CreateBankWithDepositors().GetDepositorNames());
    }

    [TestMethod]
    public void Projection_Summaries_ReturnsNamesAndAmounts()
    {
        var result = CreateBankWithDepositors().GetDepositSummaries();
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(new DepositSummary("Іван", 300m), result[1]);
    }

    [TestMethod]
    public void GroupByYear_CountsAndSumsEachGroup()
    {
        var groups = CreateBankWithDepositors().GroupDepositorsByYear();
        Assert.AreEqual(2, groups.Count);
        Assert.AreEqual(new YearDepositGroup(2023, 2, 300m), groups[0]);
        Assert.AreEqual(new YearDepositGroup(2024, 1, 300m), groups[1]);
    }

    [TestMethod]
    public void TotalAmount_ReturnsSum()
    {
        Assert.AreEqual(600m, CreateBankWithDepositors().GetTotalDepositAmount());
    }

    [TestMethod]
    public void AverageAmount_ReturnsAverage()
    {
        Assert.AreEqual(200m, CreateBankWithDepositors().GetAverageDepositAmount());
    }

    [TestMethod]
    public void EmptyBank_AggregationsReturnZero()
    {
        var bank = new Bank(new JsonFileWriter(), new JsonFileReader(),
            Path.Combine(Path.GetTempPath(), $"linq-empty-{Guid.NewGuid():N}.json"));
        Assert.AreEqual(0m, bank.GetTotalDepositAmount());
        Assert.AreEqual(0m, bank.GetAverageDepositAmount());
        Assert.AreEqual(0, bank.GroupDepositorsByYear().Count);
    }

    [TestMethod]
    public void LinqSort_DoesNotModifySourceOrder()
    {
        var bank = CreateBankWithDepositors();
        var source = bank.GetAllDepositors();
        var sorted = bank.SortByAmount(source);
        CollectionAssert.AreEqual(new[] { "UA1", "UA3", "UA2" },
            sorted.Select(d => d.AccountNumber).ToArray());
        CollectionAssert.AreEqual(new[] { "UA1", "UA2", "UA3" },
            source.Select(d => d.AccountNumber).ToArray());
    }

    [TestMethod]
    public void LinqYearFilter_ReturnsMatchingDepositors()
    {
        var bank = CreateBankWithDepositors();
        bank.FilterByYear(2023);
        Assert.AreEqual(2, bank.Deposits.Count);
        Assert.IsTrue(bank.Deposits.All(d => d.YearOpened == 2023));
    }
}
