using System;
using System.Collections.Generic;
using System.Linq;

namespace Wallet.Domain;

/// <summary>
/// Клас для пошуку та аналізу фінансових транзакцій.
/// </summary>
public class SearchEngine
{
    /// <summary>
    /// Повертає транзакції за вказаний період.
    /// </summary>
    /// <param name="account">Рахунок для пошуку.</param>
    /// <param name="startDate">Початкова дата періоду.</param>
    /// <param name="endDate">Кінцева дата періоду.</param>
    /// <returns>Колекція транзакцій.</returns>
    public IEnumerable<Transaction> GetTransactionByPeriod(Account account, DateTime startDate, DateTime endDate)
    {
        return account.GetTransactions()
            .Where(t => t.Date >= startDate && t.Date <= endDate);
    }

    /// <summary>
    /// Повертає статистику транзакцій за днями та категоріями.
    /// </summary>
    /// <param name="account">Рахунок для аналізу.</param>
    /// <param name="startDate">Початкова дата періоду.</param>
    /// <param name="endDate">Кінцева дата періоду.</param>
    /// <returns>Колекція згрупованих транзакцій.</returns>
    public IEnumerable<IGrouping<string, Transaction>> GetStatisticsByDayAndCategory(Account account, DateTime startDate, DateTime endDate)
    {
        var transactions = GetTransactionByPeriod(account, startDate, endDate);

        return transactions.GroupBy(t => $"{t.Date:yyyy-MM-dd}: {t.TransactionCategory.Name}");
    }

    /// <summary>
    /// Шукає категорії за ключовим словом.
    /// </summary>
    /// <param name="wallet">Гаманець для пошуку.</param>
    /// <param name="keyword">Ключове слово.</param>
    /// <returns>Колекція знайдених категорій.</returns>
    public IEnumerable<Category> SearchCategoriesByName(Wallet wallet, string keyword)
    {
        return wallet.GetCategories()
            .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Шукає транзакції за назвою категорії.
    /// </summary>
    /// <param name="account">Рахунок для пошуку.</param>
    /// <param name="categoryName">Назва категорії.</param>
    /// <returns>Колекція транзакцій.</returns>
    public IEnumerable<Transaction> SearchTransactionsByCategory(Account account, string categoryName)
    {
        return account.GetTransactions()
            .Where(t => t.TransactionCategory.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Шукає транзакції за сумою.
    /// </summary>
    /// <param name="account">Рахунок для пошуку.</param>
    /// <param name="amount">Сума транзакції.</param>
    /// <returns>Колекція транзакцій.</returns>
    public IEnumerable<Transaction> SearchTransactionsByAmount(Account account, decimal amount)
    {
        return account.GetTransactions()
            .Where(t => t.Amount == amount);
    }

    /// <summary>
    /// Шукає транзакції за датою.
    /// </summary>
    /// <param name="account">Рахунок для пошуку.</param>
    /// <param name="targetDate">Дата пошуку.</param>
    /// <returns>Колекція транзакцій.</returns>
    public IEnumerable<Transaction> SearchTransactionsByDate(Account account, DateTime targetDate)
    {
        return account.GetTransactions()
            .Where(t => t.Date.Date == targetDate.Date);
    }
}