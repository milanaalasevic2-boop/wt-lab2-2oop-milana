using System;

namespace wt_lab2_2oop_milana.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; private set; }
    public int CategoryId { get; set; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int categoryId, int stock)
        : base(id)
    {
        Name = name;
        UpdatePrice(price);
        UpdateStock(stock);
        CategoryId = categoryId;
    }

    public Product() : base() { }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Цена должна быть больше нуля.", nameof(newPrice));

        Price = newPrice;
    }

    public void UpdateStock(int newStock)
    {
        if (newStock < 0)
            throw new ArgumentException("Остаток не может быть отрицательным.", nameof(newStock));

        Stock = newStock;
    }

    protected override string GetDisplayName() => $"Товар «{Name}»";

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"[{Id}] {Name} — {Price:C}, категория: {CategoryId}, склад: {Stock} шт., создан: {CreatedAt:g}");
    }
}