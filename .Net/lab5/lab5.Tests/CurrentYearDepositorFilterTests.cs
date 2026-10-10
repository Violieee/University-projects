using lab5.Core.Filters;
using lab5.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class CurrentYearDepositorFilterTests
{
    [TestMethod]
    public void Filter_ReturnsOnlyGivenYearInNewList()
    {
        int currentYear = DateTime.Now.Year;
        var current = new Depositor("Поточний", "UA1", 100m, currentYear);
        var previous = new Depositor("Минулий", "UA2", 200m, currentYear - 1);
        var source = new List<Depositor> { current, previous };

        var result = new CurrentYearDepositorFilter().Filter(source, currentYear);

        Assert.AreEqual(1, result.Count);
        Assert.AreSame(current, result[0]);
        Assert.AreEqual(2, source.Count);
        Assert.AreNotSame(source, result);
    }

    [TestMethod]
    public void Filter_InvalidYear_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            new CurrentYearDepositorFilter().Filter(Array.Empty<Depositor>(), 1800));
    }
}
