namespace Wallet.Domain;

public abstract class Transaction : IFinancialRecord
{
    public Guid Id { get; private set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Category TransactionCategory { get; set; }

    protected Transaction(decimal amount, Category category)
    {
        Id = Guid.NewGuid();
        Name = name;
        TransactionCategory = category;
        Date = DateTime.Now;
    }
}