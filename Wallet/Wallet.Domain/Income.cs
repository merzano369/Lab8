namespace Wallet.Domain;

public class Income : Transaction
{
    public Income(decimal amount, Category category) : base(amount, category)
    {

    }
}