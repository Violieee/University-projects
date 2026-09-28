using System;

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

    public virtual void Pay()
    {
        Console.WriteLine("Оренду оплачено.");
    }

    public virtual void TerminateContract()
    {
        Console.WriteLine("Договір оренди розірвано.");
    }
}
