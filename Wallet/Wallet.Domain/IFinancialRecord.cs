namespace Wallet.Domain;

public interface IFinancialRecord
{
    decimal Amount { get; set; }
    DateTime Date { get; set; }
}
