using lab5.Core.Comparers;
using lab5.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class AmountComparerTests
{
    [TestMethod]
    public void Compare_SortsByAmountAscending()
    {
        var low = new Depositor("А", "UA1", 100m, 2024);
        var high = new Depositor("Б", "UA2", 200m, 2024);
        var comparer = new AmountComparer();
        Assert.IsTrue(comparer.Compare(low, high) < 0);
        Assert.IsTrue(comparer.Compare(high, low) > 0);
    }

    [TestMethod]
    public void Compare_EqualAmount_SortsByName()
    {
        var a = new Depositor("Анна", "UA1", 100m, 2024);
        var b = new Depositor("Богдан", "UA2", 100m, 2024);
        Assert.IsTrue(new AmountComparer().Compare(a, b) < 0);
    }

    [TestMethod]
    public void Compare_Nulls_AreHandled()
    {
        var a = new Depositor("Анна", "UA1", 100m, 2024);
        var comparer = new AmountComparer();
        Assert.AreEqual(0, comparer.Compare(null, null));
        Assert.IsTrue(comparer.Compare(null, a) < 0);
        Assert.IsTrue(comparer.Compare(a, null) > 0);
    }
}
