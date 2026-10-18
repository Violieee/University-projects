namespace lab5.Core.Models;

public record DepositSummary(string FullName, decimal Amount);
public record YearDepositGroup(int Year, int DepositorCount, decimal TotalAmount);
