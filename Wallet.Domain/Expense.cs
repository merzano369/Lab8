namespace Wallet.Domain;

public class Expense : Transaction
{
    public Expense(decimal amount, Category category) : base(amount, category)
    {
    }
}