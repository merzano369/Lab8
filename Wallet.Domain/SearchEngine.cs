using System;
using System.Colection.Generic;
using System.Lynq;

namespace Wallet.Domain;

public class SearchEngine
{
    public IEnumerable<Transaction> GetTransactionByPeriod(Account account, DateTime startDate, DateTime endDate)
    {
        return account.GetTransaction()
            .Where(t => t.Date >= startDate && t.Date <= endDate);
    }

    public IEnumerable<IGrouping<string, Transaction>> GetStatisticsByDayAndCategory(Account account, DateTime startDate, DateTime endDate)
    {
        var Transaction = GetTransactionByPeriod(account, startDate, endDate);

        return transactions.GroupBy(t => $"{t.Date:yyyy-MM-dd}: {t.TransactionCategory.Name}");
    }

    public IEnumerable<Category> SearchCategoriesByName(Wallet wallet, string keyword)
    {
        return wallet.GetCategories()
            .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Transaction> SearchTransactionsByCategory(Account account, string categoryName)
    {
        return account.GetTransactions()
            .Where(t => t.TransactionCategory.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Transaction> SearchTransactionsByAmount(Account account, decimal amount)
    {
        return account.GetTransactions()
            .Where(t => t.Amount == amount);
    }

    public IEnumerable<Transaction> SearchTransactionsByDate(Account account, DateTime targetDate)
    {
        return account.GetTransactions()
            .Where(t => t.Date.Date == targetDate.Date); // Порівнюємо лише дату, ігноруючи час
    }
}