namespace wt_lab2_2oop_milana.Models;

public class Accessory : Product
{
    public string AccessoryType { get; set; } = string.Empty;

    public Accessory(int id, string name, decimal price, int categoryId, int stock,
                     string accessoryType)
        : base(id, name, price, categoryId, stock)
    {
        AccessoryType = accessoryType;
    }

    protected override string GetDisplayName() => $"Аксессуар «{Name}»";

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"    Тип: {AccessoryType}");
    }
}