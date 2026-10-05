namespace lab4.RealEstateRentalApp;

public class House : RealEstateRental, IInfrastructureEvaluable
{
    public bool HasGarden { get; set; }
    public bool PublicTransportNearby { get; set; }
    public bool ShopNearby { get; set; }
    public bool SchoolNearby { get; set; }

    public House(string address, decimal price, double area, bool hasGarden)
        : base(address, price, area)
    {
        HasGarden = hasGarden;
    }

    public string EvaluateInfrastructure()
    {
        int score = 0;

        if (PublicTransportNearby)
            score++;

        if (ShopNearby)
            score++;

        if (SchoolNearby)
            score++;

        return score switch
        {
            3 => "Інфраструктура розвинена добре.",
            2 => "Інфраструктура розвинена задовільно.",
            1 => "Інфраструктура розвинена слабко.",
            _ => "Інфраструктура практично відсутня."
        };
    }
}
