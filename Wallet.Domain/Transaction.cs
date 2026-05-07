namespace Wallet.Domain;

/// <summary>
/// Базовий клас для представлення фінансової транзакції.
/// </summary>
public abstract class Transaction : IFinancialRecord
{
    /// <summary>
    /// Унікальний ідентифікатор транзакції.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Сума транзакції.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата транзакції.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Категорія транзакції.
    /// </summary>
    public Category TransactionCategory { get; set; }

    /// <summary>
    /// Створює нову транзакцію.
    /// </summary>
    /// <param name="amount">Сума транзакції.</param>
    /// <param name="category">Категорія транзакції.</param>
    protected Transaction(decimal amount, Category category)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        TransactionCategory = category;
        Date = DateTime.Now;
    }
}