using System.Collections.Generic;
using System.Windows;
using lab3.RealEstateRentalApp;

namespace lab3.WPF;

public partial class MainWindow : Window
{
    private readonly List<RealEstateRental> rentals = new();
    private readonly List<bool> paidStatuses = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddApartment_Click(object sender, RoutedEventArgs e)
    {
        if (!ReadCommonData(
                out string address,
                out decimal price,
                out double area))
        {
            return;
        }

        if (!int.TryParse(RoomsTextBox.Text, out int rooms) || rooms <= 0)
        {
            MessageBox.Show("Кількість кімнат повинна бути більшою за 0.");
            return;
        }

        Apartment apartment =
            new Apartment(address, price, area, rooms);

        rentals.Add(apartment);
        paidStatuses.Add(false);

        decimal utilities = apartment.CalculateUtilities();

        RentalListBox.Items.Add(
            $"Квартира | {address} | {price} грн | " +
            $"{area} м² | Кімнат: {rooms} | Комунальні: {utilities:F2} грн");

        ClearFields();
    }

    private void AddHouse_Click(object sender, RoutedEventArgs e)
    {
        if (!ReadCommonData(
                out string address,
                out decimal price,
                out double area))
        {
            return;
        }

        bool hasGarden = GardenCheckBox.IsChecked == true;
        bool hasTransport = TransportCheckBox.IsChecked == true;
        bool hasShop = ShopCheckBox.IsChecked == true;
        bool hasSchool = SchoolCheckBox.IsChecked == true;

        House house =
            new House(address, price, area, hasGarden);

        rentals.Add(house);
        paidStatuses.Add(false);

        string infrastructure = house.EvaluateInfrastructure(
            hasTransport,
            hasShop,
            hasSchool);

        RentalListBox.Items.Add(
            $"Будинок | {address} | {price} грн | " +
            $"{area} м² | Сад: {(hasGarden ? "Так" : "Ні")} | {infrastructure}");

        ClearFields();
    }

    private void Calculate_Click(object sender, RoutedEventArgs e)
    {
        RentalCalculator calculator =
            new RentalCalculator(rentals);

        decimal result =
            calculator.CalculateAveragePricePerSquareMeter();

        ResultTextBlock.Text =
            $"Середня вартість за 1 м²: {result:F2} грн";
    }

    private void Pay_Click(object sender, RoutedEventArgs e)
    {
        int index = RentalListBox.SelectedIndex;

        if (index < 0)
        {
            MessageBox.Show("Оберіть об'єкт зі списку.");
            return;
        }

        if (paidStatuses[index])
        {
            MessageBox.Show("Цей об'єкт уже оплачено.");
            return;
        }

        rentals[index].Pay();
        paidStatuses[index] = true;

        string currentText = RentalListBox.Items[index].ToString()!;
        RentalListBox.Items[index] = currentText + " | ОПЛАЧЕНО";

        MessageBox.Show("Оренду оплачено.");
    }

    private void Terminate_Click(object sender, RoutedEventArgs e)
    {
        int index = RentalListBox.SelectedIndex;

        if (index < 0)
        {
            MessageBox.Show("Оберіть об'єкт зі списку.");
            return;
        }

        rentals[index].TerminateContract();

        rentals.RemoveAt(index);
        paidStatuses.RemoveAt(index);
        RentalListBox.Items.RemoveAt(index);

        ResultTextBlock.Text = "";

        MessageBox.Show("Договір оренди розірвано.");
    }

    private bool ReadCommonData(
        out string address,
        out decimal price,
        out double area)
    {
        address = AddressTextBox.Text;

        if (string.IsNullOrWhiteSpace(address))
        {
            MessageBox.Show("Введіть адресу.");

            price = 0;
            area = 0;

            return false;
        }

        if (!decimal.TryParse(PriceTextBox.Text, out price) || price <= 0)
        {
            MessageBox.Show("Вартість повинна бути більшою за 0.");
            area = 0;
            return false;
        }

        if (!double.TryParse(AreaTextBox.Text, out area) || area <= 0)
        {
            MessageBox.Show("Площа повинна бути більшою за 0.");
            return false;
        }

        return true;
    }

    private void ClearFields()
    {
        AddressTextBox.Clear();
        PriceTextBox.Clear();
        AreaTextBox.Clear();
        RoomsTextBox.Clear();

        GardenCheckBox.IsChecked = false;
        TransportCheckBox.IsChecked = false;
        ShopCheckBox.IsChecked = false;
        SchoolCheckBox.IsChecked = false;
    }
}
