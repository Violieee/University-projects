namespace lab4.RealEstateRentalApp;

public abstract class RealEstateRental : IRentable
{
    public string Address { get; set; }
    public decimal MonthlyPrice { get; set; }
    public double TotalArea { get; set; }

    public bool IsPaid { get; private set; }
    public bool IsContractActive { get; private set; } = true;

    protected RealEstateRental(
        string address,
        decimal monthlyPrice,
        double totalArea)
    {
        Address = address;
        MonthlyPrice = monthlyPrice;
        TotalArea = totalArea;
    }

    public void Pay()
    {
        if (!IsContractActive)
            return;

        IsPaid = true;
    }

    public void TerminateContract()
    {
        IsContractActive = false;
    }
}