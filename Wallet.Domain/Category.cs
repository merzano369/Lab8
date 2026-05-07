using System.Diagnostics.Contracts;

namespace Wallet.Domain;

/// <summary>
/// Клас для представлення категорії фінансів.
/// </summary>
public class Category
{
    /// <summary>
    /// Унікальний ідентифікатор категорії.
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Назва категорії.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Створює нову категорію.
    /// </summary>
    /// <param name="name">Назва категорії.</param>
    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}