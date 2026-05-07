namespace Wallet.Domain;

/// <summary>
/// Клас для представлення прибутку.
/// </summary>
public class Income : Transaction
{
    /// <summary>
    /// Створює новий прибуток.
    /// </summary>
    /// <param name="amount">Сума прибутку.</param>
    /// <param name="category">Категорія прибутку.</param>
    public Income(decimal amount, Category category) : base(amount, category)
    {
    }
}