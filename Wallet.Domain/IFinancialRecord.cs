namespace Wallet.Domain;

/// <summary>
/// Інтерфейс для представлення фінансового запису.
/// </summary>
public interface IFinancialRecord
{
    /// <summary>
    /// Сума фінансового запису.
    /// </summary>
    decimal Amount { get; set; }
    
    /// <summary>
    /// Дата фінансового запису.
    /// </summary>
    DateTime Date { get; set; }
}
