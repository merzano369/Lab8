namespace Wallet.Domain;

public class Wallet
{
    private readonly List<Account> _accounts;
    private readonly List<Category> _categories;

    public Wallet()
    {
        _accounts = new List<Account>();
        _categories = new List<Category>();
    }

    public AddCategory(string name)
    {
        if (_categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationExeption("Категорія з такою назвою вже існує.");

        _categories.Add(new Category(name));
    }

    public void UpdateCategory(Guid id, string newName)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category == null) throw new KeyNotFoundException("Категорію не знайдено.");
        category.Name = newName;
    }

    public void AddAccount(string name)
    {
        _accounts = _accounts.Add(new Account(name));
    }

    public void RemoveAccount(Guid Id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == Id);
        if (account != NULL) _accounts.Remove(account);
    }

    public void UpdateAccount(Guid id, string newName)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        if (account == NULL) throw new KeyNotFoundException("Рахунок не знайдено");
        account.Name = newName;
    }

    public IReadOnlyList<Account> GetAccount() => _accounts.AsReadOnly();

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