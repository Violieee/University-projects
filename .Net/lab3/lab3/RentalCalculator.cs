using System;

namespace lab3.RealEstateRentalApp;

public class RentalCalculator
{
    private List<RealEstateRental> rentals;

    public RentalCalculator(List<RealEstateRental> rentals)
    {
        this.rentals = rentals;
    }

    public decimal CalculateAveragePricePerSquareMeter()
    {
        if (rentals.Count == 0)
            return 0;

        decimal sum = 0;

        foreach (var rental in rentals)
        {
            sum += rental.MonthlyPrice / (decimal)rental.TotalArea;
        }

        return sum / rentals.Count;
    }
}