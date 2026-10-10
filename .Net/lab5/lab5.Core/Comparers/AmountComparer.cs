using lab5.Core.Models;

namespace lab5.Core.Comparers;

public class AmountComparer : IComparer<Depositor>
{
    public int Compare(Depositor? x, Depositor? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        int amountResult = x.Amount.CompareTo(y.Amount);
        if (amountResult != 0) return amountResult;

        int nameResult = StringComparer.CurrentCultureIgnoreCase.Compare(x.FullName, y.FullName);
        return nameResult != 0 ? nameResult
            : StringComparer.OrdinalIgnoreCase.Compare(x.AccountNumber, y.AccountNumber);
    }
}
