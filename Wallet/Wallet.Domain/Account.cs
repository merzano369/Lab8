namespace Wallet.Domain;

public class Account
{
    public Guid Id { get; private set; }

    public string Name { get; set; }

    public decimal Balance { get; private set; }

    private readonly List<Transaction> _transaction;

    public Account(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = 0;
        _transaction = new List<Transaction>();
    }

    public void AddIncome(Income income)
    {
        _transactions.AddIncome(income);
        Balance += income.Amount;
    }

    public void AddExpense(Expense expense)
    {
        _transaction.AddExpense(expense);
        Balance -= expense.Amount;
    }

    public IReadOnlyList<Transaction> GetTransaction()
    {
        return _transaction.AsReadOnly();
    }
}