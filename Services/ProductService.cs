using wt_lab2_2oop_milana.Interfaces;
using wt_lab2_2oop_milana.Models;

namespace wt_lab2_2oop_milana.Services;

public class ProductService : IItemService<Product>
{
    private readonly List<Product> _products = new();
    private readonly Dictionary<int, Product> _byId = new();
    private int _nextId = 1;

    public ProductService()
    {
        Seed();
    }

    private void Seed()
    {
        Add(new Laptop(_nextId++, "Lenovo IdeaPad 5", 74999m, 1, 10, "Ryzen 7", 16));
        Add(new Smartphone(_nextId++, "Samsung Galaxy S24", 89999m, 2, 15, 6.2, 4000));
        Add(new Accessory(_nextId++, "Logitech MX Master 3S", 9990m, 3, 30, "Мышь"));
        Add(new Laptop(_nextId++, "Apple MacBook Air M3", 129999m, 1, 7, "M3", 8));
        Add(new Smartphone(_nextId++, "Xiaomi 14", 69999m, 2, 12, 6.36, 4610));
    }

    public void Add(Product item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (_byId.ContainsKey(item.Id))
            throw new InvalidOperationException($"Товар с Id={item.Id} уже существует");

        _products.Add(item);
        _byId.Add(item.Id, item);
    }

    public Product Add(string name, decimal price, int categoryId, int stock)
    {
        var p = new Product(_nextId++, name, price, categoryId, stock);
        Add(p);
        return p;
    }

    public bool Remove(int id)
    {
        if (!_byId.TryGetValue(id, out var p)) return false;
        _byId.Remove(id);
        _products.Remove(p);
        return true;
    }

    public Product? GetById(int id)
    {
        _byId.TryGetValue(id, out var p);
        return p;
    }

    public IEnumerable<Product> GetAll() => _products;

    public IEnumerable<Product> Find(Func<Product, bool> predicate)
        => _products.Where(predicate);
    

    // Уровень 1: async Task — имитация долгой операции
    public async Task LoadDataAsync()
    {
        Console.WriteLine("  [async] Начинаю загрузку данных...");
        await Task.Delay(1000);   // 1 секунда "работы"
        Console.WriteLine("  [async] Данные загружены.");
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            Console.WriteLine("  [async] Загрузка каталога из внешнего API...");
            await Task.Delay(1200, cancellationToken);
            Console.WriteLine("  [async] Каталог получен.");
            return _products.ToList();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("  [async] Загрузка отменена пользователем.");
            return new List<Product>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [async] Ошибка загрузки: {ex.Message}");
            return new List<Product>();
        }
        finally
        {
            Console.WriteLine("  [async] finally: освобождение ресурсов.");
        }
    }

    public async Task<Product?> LoadProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(600, cancellationToken);
        _byId.TryGetValue(id, out var p);
        return p;
    }
}