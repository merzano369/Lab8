namespace Wallet.Domain;

/// <summary>
/// Клас для представлення банківського рахунку.
/// </summary>
public class Account
{
    /// <summary>
    /// Унікальний ідентифікатор рахунку.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Назва рахунку.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Поточний баланс рахунку.
    /// </summary>
    public decimal Balance { get; private set; }

    private readonly List<Transaction> _transactions;

    /// <summary>
    /// Створює новий рахунок з вказаною назвою.
    /// </summary>
    /// <param name="name">Назва рахунку.</param>
    public Account(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = 0;
        _transactions = new List<Transaction>();
    }

    /// <summary>
    /// Додає прибуток до рахунку.
    /// </summary>
    /// <param name="income">Об'єкт прибутку.</param>
    public void AddIncome(Income income)
    {
        _transactions.Add(income);
        Balance += income.Amount;
    }

    /// <summary>
    /// Додає витрату до рахунку.
    /// </summary>
    /// <param name="expense">Об'єкт витрати.</param>
    public void AddExpense(Expense expense)
    {
        if (expense.Amount > Balance)
        {
            throw new InvalidOperationException($"Недостатньо коштів на рахунку '{Name}'.");
        }

        _transactions.Add(expense);
        Balance -= expense.Amount;
    }

    /// <summary>
    /// Повертає список всіх транзакцій рахунку.
    /// </summary>
    /// <returns>Список транзакцій.</returns>
    public IReadOnlyList<Transaction> GetTransactions()
    {
        return _transactions.AsReadOnly();
    }

    /// <summary>
    /// Видаляє транзакцію з рахунку та перераховує баланс.
    /// </summary>
    /// <param name="transactionId">Ідентифікатор транзакції.</param>
    public void RemoveTransaction(Guid transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction == null) 
            throw new KeyNotFoundException("Транзакцію не знайдено.");

        if (transaction is Income)
        {
            Balance -= transaction.Amount;
        }
        else if (transaction is Expense)
        {
            Balance += transaction.Amount;
        }

        _transactions.Remove(transaction);
    }
}