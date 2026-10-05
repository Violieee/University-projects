using System;

namespace lab3.RealEstateRentalApp;

public class House : RealEstateRental
{
    public bool HasGarden { get; set; }

    public House(string address, decimal price, double area, bool hasGarden)
        : base(address, price, area)
    {
        HasGarden = hasGarden;
    }

    public override string EvaluateInfrastructure(
        bool publicTransportNearby,
        bool shopNearby,
        bool schoolNearby)
    {
        int score = 0;

        if (publicTransportNearby)
            score++;

        if (shopNearby)
            score++;

        if (schoolNearby)
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
