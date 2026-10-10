using System.Globalization;
using System.Windows;
using lab5.Core.FileStorage;
using lab5.Core.Models;
using lab5.Core.Services;
using System.IO;
using System;

namespace lab5;

public partial class MainWindow : Window
{
    private readonly Bank _bank;
    private readonly string _filePath;

    public MainWindow()
    {
        InitializeComponent();

        string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _filePath = Path.Combine(documents, "lab5", "deposits.json");
        _bank = new Bank(new JsonFileWriter(), new JsonFileReader(), _filePath);
        DataContext = _bank;
        FilterYearTextBox.Text = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!TryReadAmount(AmountTextBox.Text, out decimal amount))
                throw new ArgumentException("Введіть коректну суму вкладу (наприклад, 1500,50).");
            if (!int.TryParse(YearOpenedTextBox.Text, out int year))
                throw new ArgumentException("Рік відкриття рахунку має бути цілим числом.");

            var depositor = new Depositor(FullNameTextBox.Text, AccountNumberTextBox.Text, amount, year);
            _bank.AddDepositor(depositor);

            FullNameTextBox.Clear();
            AccountNumberTextBox.Clear();
            AmountTextBox.Clear();
            YearOpenedTextBox.Clear();
            FullNameTextBox.Focus();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DepositorsDataGrid.SelectedItem is not Depositor selected)
        {
            MessageBox.Show("Виберіть вкладника в таблиці.", "Видалення",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        _bank.RemoveDepositor(selected.AccountNumber);
    }

    private void SortByAmountButton_Click(
        object sender, RoutedEventArgs e)
    {
        _bank.SortVisibleByAmount();
        FilterYearTextBox.Clear();
    }

    private void FilterButton_Click(object sender, RoutedEventArgs e)
    {
        string yearText = FilterYearTextBox.Text.Trim();
        if (yearText.Length == 0)
        {
            _bank.ResetFilter();
            return;
        }

        if (!int.TryParse(yearText, out int year))
        {
            MessageBox.Show("Введіть коректний рік або очистіть поле для скидання фільтра.",
                "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            _bank.FilterByYear(year);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            SaveButton.IsEnabled = false;
            await _bank.SaveAsync();
            MessageBox.Show($"Дані успішно збережено.\n{_filePath}", "Збереження",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
        {
            MessageBox.Show($"Не вдалося зберегти файл: {ex.Message}", "Помилка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            SaveButton.IsEnabled = true;
        }
    }

    private async void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists(_filePath))
        {
            MessageBox.Show("Файл даних ще не створено. Спочатку додайте вкладників і натисніть «Зберегти».",
                "Завантаження", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        try
        {
            LoadButton.IsEnabled = false;
            await _bank.LoadAsync();
            FilterYearTextBox.Text = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            MessageBox.Show("Дані успішно завантажено.", "Завантаження",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or
                                       System.Text.Json.JsonException or ArgumentException or InvalidDataException)
        {
            MessageBox.Show($"Не вдалося завантажити файл: {ex.Message}", "Помилка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            LoadButton.IsEnabled = true;
        }
    }

    private static bool TryReadAmount(string text, out decimal amount)
    {
        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.GetCultureInfo("uk-UA"), out amount)
            || decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out amount);
    }
}
