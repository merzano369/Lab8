namespace Wallet.Domain;

public class Account
{
    public Guid Id { get; private set; }

    public string Name { get; set; }

    public decimal Balance { get; private set; }

    private readonly List<Transaction> _transactions;

    public Account(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = 0;
        _transactions = new List<Transaction>();
    }

    public void AddIncome(Income income)
    {
        _transactions.Add(income);
        Balance += income.Amount;
    }

    public void AddExpense(Expense expense)
    {
        if (expense.Amount > Balance)
        {
            throw new InvalidOperationException($"Недостатньо коштів на рахунку '{Name}'.");
        }

        _transactions.Add(expense);
        Balance -= expense.Amount;
    }

    public IReadOnlyList<Transaction> GetTransactions()
    {
        return _transactions.AsReadOnly();
    }
}