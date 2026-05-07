namespace Wallet.Domain;

/// <summary>
/// Клас для представлення витрати.
/// </summary>
public class Expense : Transaction
{
    /// <summary>
    /// Створює нову витрату.
    /// </summary>
    /// <param name="amount">Сума витрати.</param>
    /// <param name="category">Категорія витрати.</param>
    public Expense(decimal amount, Category category) : base(amount, category)
    {
    }
}