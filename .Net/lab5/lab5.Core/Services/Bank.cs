using System.Collections.ObjectModel;
using lab5.Core.Comparers;
using lab5.Core.FileStorage;
using lab5.Core.Filters;
using lab5.Core.Models;

namespace lab5.Core.Services;

public class Bank
{
    private readonly IFileSaver _fileSaver;
    private readonly IFileLoader _fileLoader;
    private readonly string _filePath;
    private readonly CurrentYearDepositorFilter _currentYearFilter = new();
    private readonly List<Depositor> _deposits = new();
    private int? _filterYear;
    private bool _isSortedByAmount;

    public ObservableCollection<Depositor> Deposits { get; } = new();

    public Bank(IFileSaver fileSaver, IFileLoader fileLoader, string filePath)
    {
        _fileSaver = fileSaver ?? throw new ArgumentNullException(nameof(fileSaver));
        _fileLoader = fileLoader ?? throw new ArgumentNullException(nameof(fileLoader));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Шлях до файлу не може бути порожнім.", nameof(filePath));
        _filePath = filePath;
    }

    public void AddDepositor(Depositor depositor)
    {
        ArgumentNullException.ThrowIfNull(depositor);
        if (string.IsNullOrWhiteSpace(depositor.FullName)
            || string.IsNullOrWhiteSpace(depositor.AccountNumber)
            || depositor.Amount <= 0 || depositor.YearOpened < 1900
            || depositor.YearOpened > DateTime.Now.Year)
            throw new ArgumentException("Вкладник містить некоректні дані.", nameof(depositor));
        if (_deposits.Any(d => string.Equals(d.AccountNumber, depositor.AccountNumber,
                StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Вкладник із таким номером рахунку вже існує.");

        _deposits.Add(depositor);
        RefreshVisibleCollection();
    }

    public void RemoveDepositor(string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Вкажіть номер рахунку.", nameof(accountNumber));

        Depositor? depositor = _deposits.FirstOrDefault(d =>
            string.Equals(d.AccountNumber, accountNumber.Trim(), StringComparison.OrdinalIgnoreCase));
        if (depositor is null)
            throw new KeyNotFoundException("Вкладника з таким номером рахунку не знайдено.");

        _deposits.Remove(depositor);
        RefreshVisibleCollection();
    }

    public List<Depositor> GetAllDepositors() => new(_deposits);

    public List<Depositor> GetCurrentYearDepositors() =>
        _currentYearFilter.Filter(_deposits, DateTime.Now.Year);

    public List<Depositor> SortByAmount(IEnumerable<Depositor> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        var sorted = list.ToList();
        sorted.Sort(new AmountComparer());
        return sorted;
    }

    public void FilterByYear(int year)
    {
        if (year < 1900 || year > DateTime.Now.Year)
            throw new ArgumentOutOfRangeException(nameof(year), "Некоректний рік фільтрації.");
        _filterYear = year;
        _isSortedByAmount = true;
        RefreshVisibleCollection();
    }

    public void ResetFilter()
    {
        _filterYear = null;
        _isSortedByAmount = false;
        RefreshVisibleCollection();
    }

    public void SortVisibleByAmount()
    {
        _filterYear = null;
        _isSortedByAmount = true;
        RefreshVisibleCollection();
    }

    public async Task SaveAsync() => await _fileSaver.SaveAsync(_filePath, _deposits);

    public async Task LoadAsync()
    {
        List<Depositor>? loaded = await _fileLoader.LoadAsync<Depositor>(_filePath);
        if (loaded is null) return;

        if (loaded.Any(d => d is null || string.IsNullOrWhiteSpace(d.FullName)
            || string.IsNullOrWhiteSpace(d.AccountNumber)
            || d.Amount <= 0 || d.YearOpened < 1900 || d.YearOpened > DateTime.Now.Year))
            throw new InvalidDataException("Файл містить некоректні дані вкладників.");
        if (loaded.Select(d => d.AccountNumber)
            .Distinct(StringComparer.OrdinalIgnoreCase).Count() != loaded.Count)
            throw new InvalidDataException("У файлі повторюються номери рахунків.");

        _deposits.Clear();
        _deposits.AddRange(loaded);
        _filterYear = null;
        _isSortedByAmount = false;
        RefreshVisibleCollection();
    }

    private void RefreshVisibleCollection()
    {
        IEnumerable<Depositor> items = _filterYear.HasValue
            ? _currentYearFilter.Filter(_deposits, _filterYear.Value)
            : _deposits;

        if (_isSortedByAmount)
            items = SortByAmount(items);

        Deposits.Clear();
        foreach (Depositor depositor in items)
            Deposits.Add(depositor);
    }
}
