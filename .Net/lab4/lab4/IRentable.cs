namespace lab4.RealEstateRentalApp;

public interface IRentable
{
    bool IsPaid { get; }
    bool IsContractActive { get; }

    void Pay();
    void TerminateContract();
}