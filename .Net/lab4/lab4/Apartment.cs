namespace lab4.RealEstateRentalApp;

public class Apartment : RealEstateRental, IUtilityCalculable
{
    public int NumberOfRooms { get; set; }

    public Apartment(string address, decimal price, double area, int rooms)
        : base(address, price, area)
    {
        NumberOfRooms = rooms;
    }

    public decimal CalculateUtilities()
    {
        const decimal utilityRatePerSquareMeter = 40m;
        return (decimal)TotalArea * utilityRatePerSquareMeter;
    }
}
