namespace Wallet.Domain;

/// <summary>
/// Основний клас для управління гаманцем користувача.
/// </summary>
public class Wallet
{
    private readonly List<Account> _accounts;
    private readonly List<Category> _categories;

    /// <summary>
    /// Створює новий екземпляр гаманця.
    /// </summary>
    public Wallet()
    {
        _accounts = new List<Account>();
        _categories = new List<Category>();
    }

    /// <summary>
    /// Додає нову категорію до гаманця.
    /// </summary>
    /// <param name="name">Назва категорії.</param>
    public void AddCategory(string name)
    {
        if (_categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Категорія з такою назвою вже існує.");

        _categories.Add(new Category(name));
    }

    /// <summary>
    /// Оновлює назву категорії.
    /// </summary>
    /// <param name="id">Ідентифікатор категорії.</param>
    /// <param name="newName">Нова назва категорії.</param>
    public void UpdateCategory(Guid id, string newName)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category == null) throw new KeyNotFoundException("Категорію не знайдено.");
        category.Name = newName;
    }

    /// <summary>
    /// Повертає список всіх категорій.
    /// </summary>
    /// <returns>Список категорій.</returns>
    public IReadOnlyList<Category> GetCategories() => _categories.AsReadOnly();

    /// <summary>
    /// Додає новий рахунок до гаманця.
    /// </summary>
    /// <param name="name">Назва рахунку.</param>
    public void AddAccount(string name)
    {
        _accounts.Add(new Account(name));
    }

    /// <summary>
    /// Видаляє рахунок з гаманця.
    /// </summary>
    /// <param name="id">Ідентифікатор рахунку.</param>
    public void RemoveAccount(Guid id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        if (account != null) _accounts.Remove(account);
    }

    /// <summary>
    /// Оновлює назву рахунку.
    /// </summary>
    /// <param name="id">Ідентифікатор рахунку.</param>
    /// <param name="newName">Нова назва рахунку.</param>
    public void UpdateAccount(Guid id, string newName)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        if (account == null) throw new KeyNotFoundException("Рахунок не знайдено");
        account.Name = newName;
    }

    /// <summary>
    /// Повертає список всіх рахунків.
    /// </summary>
    /// <returns>Список рахунків.</returns>
    public IReadOnlyList<Account> GetAccounts() => _accounts.AsReadOnly();

    /// <summary>
    /// Переказує кошти між рахунками.
    /// </summary>
    /// <param name="fromId">Ідентифікатор рахунку відправника.</param>
    /// <param name="toId">Ідентифікатор рахунку отримувача.</param>
    /// <param name="amount">Сума переказу.</param>
    /// <param name="category">Категорія транзакції.</param>
    public void Transfer(Guid fromId, Guid toId, decimal amount, Category category)
    {
        var source = _accounts.FirstOrDefault(a => a.Id == fromId);
        var destination = _accounts.FirstOrDefault(a => a.Id == toId);

        if (source == null || destination == null)
            throw new ArgumentException("Один або обидва рахунки не знайдені.");

        source.AddExpense(new Expense(amount, category));
        destination.AddIncome(new Income(amount, category));
    }
}