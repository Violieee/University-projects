namespace lab4.RealEstateRentalApp;

public class RentalCalculator
{
    private readonly List<RealEstateRental> rentals;
    private readonly double areaMin;

    public RentalCalculator(List<RealEstateRental> rentals, double areaMin = 0)
    {
        this.rentals = rentals;
        this.areaMin = areaMin;
    }

    public decimal CalculateAverageMonthlyPrice()
    {
        List<RealEstateRental> filteredRentals = rentals
            .Where(rental => rental.TotalArea >= areaMin)
            .ToList();

        if (filteredRentals.Count == 0)
            return 0m;

        return filteredRentals.Average(rental => rental.MonthlyPrice);
    }
}
