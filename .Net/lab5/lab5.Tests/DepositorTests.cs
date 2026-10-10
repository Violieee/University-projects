using lab5.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class DepositorTests
{
    [TestMethod]
    public void Constructor_ValidArguments_CreatesDepositor()
    {
        var item = new Depositor("Іваненко Іван Іванович", "UA123456789", 4500.50m, 2024);

        Assert.AreEqual("Іваненко Іван Іванович", item.FullName);
        Assert.AreEqual("UA123456789", item.AccountNumber);
        Assert.AreEqual(4500.50m, item.Amount);
        Assert.AreEqual(2024, item.YearOpened);
    }

    [TestMethod]
    public void Constructor_EmptyFullName_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() => new Depositor(" ", "UA123", 100m, 2024));
    }

    [TestMethod]
    public void Constructor_InvalidAccount_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() => new Depositor("Олена", "@@@", 100m, 2024));
    }

    [TestMethod]
    public void Constructor_NonPositiveAmount_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Depositor("Олена", "UA123", 0m, 2024));
    }

    [TestMethod]
    public void Constructor_FutureYear_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            new Depositor("Олена", "UA123", 100m, DateTime.Now.Year + 1));
    }

    [TestMethod]
    public void CompareTo_UsesAccountNumber()
    {
        var a = new Depositor("А", "UA100", 500m, 2024);
        var b = new Depositor("Б", "UA200", 200m, 2024);
        Assert.IsTrue(a.CompareTo(b) < 0);
        Assert.IsTrue(b.CompareTo(a) > 0);
        Assert.AreEqual(0, a.CompareTo(new Depositor("В", "ua100", 10m, 2024)));
        Assert.IsTrue(a.CompareTo(null) > 0);
    }

    [TestMethod]
    public void ToString_ContainsAllFields()
    {
        var depositor = new Depositor("Марія Коваль", "UA999", 2500m, 2024);
        string result = depositor.ToString();
        StringAssert.Contains(result, "Марія Коваль");
        StringAssert.Contains(result, "UA999");
        StringAssert.Contains(result, "2024");
        StringAssert.Contains(result, "2");
    }
}
