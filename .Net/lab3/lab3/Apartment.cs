using System;

namespace lab3.RealEstateRentalApp;

public class Apartment : RealEstateRental
{
    public int NumberOfRooms { get; set; }

    public Apartment(string address, decimal price, double area, int rooms)
        : base(address, price, area)
    {
        NumberOfRooms = rooms;
    }

    public virtual decimal CalculateUtilities()
    {
        const decimal utilityRatePerSquareMeter = 40m;
        return (decimal)TotalArea * utilityRatePerSquareMeter;
    }

    public override void Pay()
    {
        Console.WriteLine("Оренду квартири оплачено.");
    }

    public override void TerminateContract()
    {
        Console.WriteLine("Договір оренди квартири розірвано.");
    }
}
