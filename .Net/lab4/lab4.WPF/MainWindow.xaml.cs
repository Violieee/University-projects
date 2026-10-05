using System.Collections.Generic;
using System.Windows;
using lab4.RealEstateRentalApp;

namespace lab4.WPF;

public partial class MainWindow : Window
{
    private readonly List<RealEstateRental> rentals = new();

    // Поліморфізм через інтерфейс: у колекції одночасно можуть бути
    // Apartment, House, Office та інші типи, що реалізують IRentable.
    private readonly List<IRentable> rentableObjects = new();

    private readonly List<bool> paidStatuses = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddApartment_Click(object sender, RoutedEventArgs e)
    {
        if (!ReadCommonData(out string address, out decimal price, out double area))
            return;

        if (!int.TryParse(RoomsTextBox.Text, out int rooms) || rooms <= 0)
        {
            MessageBox.Show("Кількість кімнат повинна бути більшою за 0.");
            return;
        }

        Apartment apartment = new(address, price, area, rooms);
        AddRental(apartment);

        IUtilityCalculable utilityObject = apartment;
        decimal utilities = utilityObject.CalculateUtilities();

        RentalListBox.Items.Add(
            $"Квартира | {address} | {price} грн/міс. | " +
            $"{area} м² | Кімнат: {rooms} | Комунальні: {utilities:F2} грн");

        ClearFields();
    }

    private void AddHouse_Click(object sender, RoutedEventArgs e)
    {
        if (!ReadCommonData(out string address, out decimal price, out double area))
            return;

        House house = new(address, price, area, GardenCheckBox.IsChecked == true)
        {
            PublicTransportNearby = TransportCheckBox.IsChecked == true,
            ShopNearby = ShopCheckBox.IsChecked == true,
            SchoolNearby = SchoolCheckBox.IsChecked == true
        };

        AddRental(house);

        IInfrastructureEvaluable infrastructureObject = house;
        string infrastructure = infrastructureObject.EvaluateInfrastructure();

        RentalListBox.Items.Add(
            $"Будинок | {address} | {price} грн/міс. | " +
            $"{area} м² | Сад: {(house.HasGarden ? "Так" : "Ні")} | {infrastructure}");

        ClearFields();
    }

    private void AddOffice_Click(object sender, RoutedEventArgs e)
    {
        if (!ReadCommonData(out string address, out decimal price, out double area))
            return;

        Office office = new(address, price, area);
        AddRental(office);

        IUtilityCalculable utilityObject = office;
        decimal utilities = utilityObject.CalculateUtilities();

        RentalListBox.Items.Add(
            $"Офіс | {address} | {price} грн/міс. | " +
            $"{area} м² | Комунальні: {utilities:F2} грн");

        ClearFields();
    }

    private void AddRental(RealEstateRental rental)
    {
        rentals.Add(rental);
        rentableObjects.Add(rental);
        paidStatuses.Add(false);
    }

    private void Calculate_Click(object sender, RoutedEventArgs e)
    {
        RentalCalculator calculator = new(rentals);
        decimal result = calculator.CalculateAverageMonthlyPrice();

        ResultTextBlock.Text = $"Середня місячна вартість оренди: {result:F2} грн";
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

        // Виклик через IRentable демонструє поліморфізм інтерфейсу.
        rentableObjects[index].Pay();
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

        // Так само працюємо з різними класами через єдиний інтерфейс IRentable.
        rentableObjects[index].TerminateContract();

        rentals.RemoveAt(index);
        rentableObjects.RemoveAt(index);
        paidStatuses.RemoveAt(index);
        RentalListBox.Items.RemoveAt(index);

        ResultTextBlock.Text = string.Empty;
        MessageBox.Show("Договір оренди розірвано.");
    }

    private bool ReadCommonData(out string address, out decimal price, out double area)
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
