using lab5.Core.Models;

namespace lab5.Core.Filters;

public class CurrentYearDepositorFilter
{
    public List<Depositor> Filter(IEnumerable<Depositor> allDepositors, int currentYear)
    {
        ArgumentNullException.ThrowIfNull(allDepositors);
        if (currentYear < 1900 || currentYear > DateTime.Now.Year)
            throw new ArgumentOutOfRangeException(nameof(currentYear), "Некоректний рік фільтрації.");

        return allDepositors.Where(d => d.YearOpened == currentYear).ToList();
    }
}
