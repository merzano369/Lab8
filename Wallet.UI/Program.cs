using System;
using System.Linq;
using Wallet.Domain;

namespace Wallet.UI;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Демонстрація роботи системи 'Гаманець' (Варіант 15) ===\n");

        var myWallet = new Domain.Wallet();
        var searchEngine = new SearchEngine();

        myWallet.AddCategory("Зарплата");
        myWallet.AddCategory("Продукти");
        myWallet.AddCategory("Розваги");
        myWallet.AddCategory("Переказ");

        var categories = myWallet.GetCategories();
        var salaryCat = categories.First(c => c.Name == "Зарплата");
        var foodCat = categories.First(c => c.Name == "Продукти");
        var transferCat = categories.First(c => c.Name == "Переказ");

        myWallet.AddAccount("Картка Монобанк");
        myWallet.AddAccount("Готівка");

        var accounts = myWallet.GetAccounts();
        var monoCard = accounts.First(a => a.Name == "Картка Монобанк");
        var cash = accounts.First(a => a.Name == "Готівка");

        Console.WriteLine("-> Нарахування зарплати та перші витрати...");
        monoCard.AddIncome(new Income(30000m, salaryCat));
        monoCard.AddExpense(new Expense(1500m, foodCat));

        Console.WriteLine("-> Зняття готівки (переказ з Монобанку на Готівку)...");
        myWallet.Transfer(monoCard.Id, cash.Id, 5000m, transferCat);

        Console.WriteLine("\n--- Поточні баланси ---");
        Console.WriteLine($"{monoCard.Name}: {monoCard.Balance} грн");
        Console.WriteLine($"{cash.Name}: {cash.Balance} грн");

        Console.WriteLine("\n--- Статистика по Картці Монобанк за сьогодні ---");
        var stats = searchEngine.GetStatisticsByDayAndCategory(
            monoCard,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1)
        );

        foreach (var group in stats)
        {
            Console.WriteLine($"[{group.Key}]");
            foreach (var transaction in group)
            {
                string type = transaction is Income ? "+Прибуток" : "-Витрата";
                Console.WriteLine($"   {type}: {transaction.Amount} грн");
            }
        }

        Console.WriteLine("\n--- Демонстрація обробки помилок ---");
        try
        {
            Console.WriteLine("Спроба купити щось за 10 000 грн за готівку...");
            cash.AddExpense(new Expense(10000m, foodCat));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Виключення перехоплено: {ex.Message}");
        }

        Console.WriteLine("\nРоботу завершено. Натисніть Enter для виходу.");
        Console.ReadLine();
    }
}