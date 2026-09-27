namespace wt_lab2_2oop_milana.Models;

public class Laptop : Product
{
    public string Cpu { get; set; } = string.Empty;
    public int RamGb { get; set; }

    public Laptop(int id, string name, decimal price, int categoryId, int stock,
                  string cpu, int ramGb)
        : base(id, name, price, categoryId, stock)
    {
        Cpu = cpu;
        RamGb = ramGb;
    }

    protected override string GetDisplayName() => $"Ноутбук «{Name}»";

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"    CPU: {Cpu}, RAM: {RamGb} GB");
    }
}