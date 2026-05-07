using System;
using System.Linq;
using Wallet.Domain; // Підключаємо нашу бібліотеку з логікою

namespace Wallet.UI;

class Program
{
    static void Main(string[] args)
    {
        // Налаштування для коректного відображення української мови в консолі
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Демонстрація роботи системи 'Гаманець' (Варіант 15) ===\n");

        // 1. Ініціалізація системи
        var myWallet = new Domain.Wallet();
        var searchEngine = new SearchEngine();

        // 2. Додавання категорій (Вимога 1.1)
        myWallet.AddCategory("Зарплата");
        myWallet.AddCategory("Продукти");
        myWallet.AddCategory("Розваги");
        myWallet.AddCategory("Переказ"); // Системна категорія для переказів

        var categories = myWallet.GetCategories();
        var salaryCat = categories.First(c => c.Name == "Зарплата");
        var foodCat = categories.First(c => c.Name == "Продукти");
        var transferCat = categories.First(c => c.Name == "Переказ");

        // 3. Додавання рахунків (Вимога 2.1)
        myWallet.AddAccount("Картка Монобанк");
        myWallet.AddAccount("Готівка");

        var accounts = myWallet.GetAccounts();
        var monoCard = accounts.First(a => a.Name == "Картка Монобанк");
        var cash = accounts.First(a => a.Name == "Готівка");

        // 4. Додавання прибутків та витрат (Вимоги 3.1, 3.2)
        Console.WriteLine("-> Нарахування зарплати та перші витрати...");
        monoCard.AddIncome(new Income(30000m, salaryCat));
        monoCard.AddExpense(new Expense(1500m, foodCat));

        // 5. Переказ коштів між рахунками (Вимога 3.3)
        Console.WriteLine("-> Зняття готівки (переказ з Монобанку на Готівку)...");
        myWallet.Transfer(monoCard.Id, cash.Id, 5000m, transferCat);

        // Перегляд балансу (Вимога 2.5)
        Console.WriteLine("\n--- Поточні баланси ---");
        Console.WriteLine($"{monoCard.Name}: {monoCard.Balance} грн");
        Console.WriteLine($"{cash.Name}: {cash.Balance} грн");

        // 6. Статистика (Вимога 3.5)
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

        // 7. Демонстрація обробки виключень
        Console.WriteLine("\n--- Демонстрація обробки помилок ---");
        try
        {
            Console.WriteLine("Спроба купити щось за 10 000 грн за готівку...");
            cash.AddExpense(new Expense(10000m, foodCat)); // На готівці лише 5000
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Виключення перехоплено: {ex.Message}");
        }

        Console.WriteLine("\nРоботу завершено. Натисніть Enter для виходу.");
        Console.ReadLine();
    }
}