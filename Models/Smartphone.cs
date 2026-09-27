namespace wt_lab2_2oop_milana.Models;

public class Smartphone : Product
{
    public double ScreenSize { get; set; }
    public int BatteryMah { get; set; }

    public Smartphone(int id, string name, decimal price, int categoryId, int stock,
                      double screenSize, int batteryMah)
        : base(id, name, price, categoryId, stock)
    {
        ScreenSize = screenSize;
        BatteryMah = batteryMah;
    }

    protected override string GetDisplayName() => $"Смартфон «{Name}»";

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"    Экран: {ScreenSize}\", батарея: {BatteryMah} mAh");
    }
}