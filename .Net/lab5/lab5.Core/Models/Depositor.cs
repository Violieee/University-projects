using System.Globalization;
using System.Text.RegularExpressions;

namespace lab5.Core.Models;

public class Depositor : IComparable<Depositor>
{
    private string _fullName = string.Empty;
    private string _accountNumber = string.Empty;
    private decimal _amount;
    private int _yearOpened;

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ПІБ вкладника не може бути порожнім.", nameof(FullName));
            if (value.Trim().Length > 150)
                throw new ArgumentException("ПІБ не може бути довшим за 150 символів.", nameof(FullName));
            _fullName = value.Trim();
        }
    }

    public string AccountNumber
    {
        get => _accountNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер рахунку не може бути порожнім.", nameof(AccountNumber));

            string normalized = value.Trim().Replace(" ", string.Empty).ToUpperInvariant();
            if (normalized.Length > 34 || !Regex.IsMatch(normalized, @"^[A-Z0-9-]+$"))
                throw new ArgumentException("Номер рахунку має містити 1–34 латинські літери, цифри або дефіс.", nameof(AccountNumber));
            _accountNumber = normalized;
        }
    }

    public decimal Amount
    {
        get => _amount;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(Amount), "Сума вкладу повинна бути більшою за нуль.");
            _amount = value;
        }
    }

    public int YearOpened
    {
        get => _yearOpened;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(YearOpened),
                    $"Рік відкриття повинен бути від 1900 до {DateTime.Now.Year}.");
            _yearOpened = value;
        }
    }

    public Depositor() { }

    public Depositor(string fullName, string accountNumber, decimal amount, int yearOpened)
    {
        FullName = fullName;
        AccountNumber = accountNumber;
        Amount = amount;
        YearOpened = yearOpened;
    }

    public int CompareTo(Depositor? other) =>
        other is null ? 1 : StringComparer.OrdinalIgnoreCase.Compare(AccountNumber, other.AccountNumber);

    public override string ToString() =>
        $"{FullName}; рахунок: {AccountNumber}; " +
        $"сума: {Amount.ToString("N2", CultureInfo.GetCultureInfo("uk-UA"))} грн; " +
        $"рік відкриття: {YearOpened}";
}
