namespace lab4.RealEstateRentalApp;
public class Office : RealEstateRental, IUtilityCalculable
{
    public Office(string address, decimal price, double area)
        : base(address, price, area)
    {
    }

    public decimal CalculateUtilities()
    {
        const decimal utilityRatePerSquareMeter = 55m;
        return (decimal)TotalArea * utilityRatePerSquareMeter;
    }
}
