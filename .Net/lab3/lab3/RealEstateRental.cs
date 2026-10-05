using System;
using System.Reflection.Metadata.Ecma335;

namespace lab3.RealEstateRentalApp;

public abstract class RealEstateRental
{
    public string Address { get; set; }
    public decimal MonthlyPrice { get; set; }
    public double TotalArea { get; set; }

    protected RealEstateRental(string address, decimal monthlyPrice, double totalArea)
    {
        Address = address;
        MonthlyPrice = monthlyPrice;
        TotalArea = totalArea;
    }
    public virtual decimal CalculateUtilities()
    {
        return 0;
    }

    public virtual string EvaluateInfrastructure(
        bool publicTransportNearby,
        bool shopNearby,
        bool schoolNearby)
    {
        return "";
    }

    public virtual void Pay()
    {
    }

    public virtual void TerminateContract()
    {
    }
}
