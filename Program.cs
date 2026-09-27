using System.Text;
using wt_lab2_2oop_milana.Interfaces;
using wt_lab2_2oop_milana.Models;
using wt_lab2_2oop_milana.Services;

namespace wt_lab2_2oop_milana;

public static class Program
{
    public static async Task Main()
    {
        // Корректный вывод кириллицы в консоли Windows
        Console.OutputEncoding = Encoding.UTF8;

        DemonstrateLevel1();
        DemonstrateLevel2();
        DemonstrateLevel3();
        DemonstrateBlock2();
        await DemonstrateBlock3();

        PrintWelcome();

        bool exit = false;

        while (!exit)
        {
            PrintMenu();

            Console.Write("Выберите пункт: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("→ [заглушка] Показать все товары (Блок 2)");
                    break;
                case "2":
                    Console.WriteLine("→ [заглушка] Найти товар по Id (Блок 2)");
                    break;
                case "3":
                    Console.WriteLine("→ [заглушка] Добавить товар (Блок 1, Ур.3)");
                    break;
                case "4":
                    Console.WriteLine("→ [заглушка] LINQ-запросы (Блок 2, Ур.3)");
                    break;
                case "5":
                    Console.WriteLine("→ [заглушка] Асинхронная загрузка (Блок 3)");
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный пункт меню. Повторите ввод.");
                    break;
            }
        }

        Console.WriteLine("\nРабота приложения завершена. До свидания!");
    }

    private static void PrintWelcome()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  Лабораторная работа №2");
        Console.WriteLine("  Классы. Свойства. Методы. Экземпляры объектов");
        Console.WriteLine("  Вариант 1: Интернет-магазин электроники");
        Console.WriteLine("  Сущности: Product, Category, Order, Customer");
        Console.WriteLine("==================================================");
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("---------------- МЕНЮ ----------------");
        Console.WriteLine("1. Показать все товары");
        Console.WriteLine("2. Найти товар по Id");
        Console.WriteLine("3. Добавить товар");
        Console.WriteLine("4. LINQ: фильтрация / сортировка / статистика");
        Console.WriteLine("5. Асинхронная загрузка");
        Console.WriteLine("0. Выход");
        Console.WriteLine("--------------------------------------");
    }
    private static void DemonstrateLevel1()
    {
        Console.WriteLine("=== Блок 1, Уровень 1: класс Product ===\n");

        var laptop = new Product(1, "Lenovo IdeaPad 5", 74999m, 1, 10);
        var phone = new Product(2, "Samsung Galaxy S24", 89999m, 2, 15);

       var mouse = new Product
{
    Id = 3,
    Name = "Logitech MX Master 3S",
    CategoryId = 3
};
mouse.UpdatePrice(9990m);
mouse.UpdateStock(30);
        
        laptop.PrintInfo();
        phone.PrintInfo();
        mouse.PrintInfo();

        Console.WriteLine("\n=== Конец демонстрации ===\n");
    }
    
        private static void DemonstrateLevel2()
    {
        Console.WriteLine("=== Блок 1, Уровень 2: инкапсуляция, наследование, полиморфизм ===\n");

        Console.WriteLine("-- Инкапсуляция --");
        var product = new Product(10, "Тестовый товар", 1000m, 1, 5);

        product.UpdatePrice(1500m);
        product.UpdateStock(20);
        product.PrintInfo();

        try
        {
            product.UpdatePrice(-100m);   // специально плохая цена
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  Валидация сработала: {ex.Message}");
        }

        Console.WriteLine("\n-- Полиморфизм (BaseEntity[] + override PrintInfo) --");

        BaseEntity[] items =
        {
            new Laptop(20, "Lenovo IdeaPad 5", 74999m, 1, 10, "Ryzen 7", 16),
            new Smartphone(21, "Samsung Galaxy S24", 89999m, 2, 15, 6.2, 4000),
            new Accessory(22, "Logitech MX Master 3S", 9990m, 3, 30, "Мышь")
        };

        foreach (var item in items)
        {
            item.PrintInfo();   
            Console.WriteLine();
        }

        Console.WriteLine("=== Конец Уровня 2 ===\n");
    }
        private static void DemonstrateLevel3()
    {
        Console.WriteLine("=== Блок 1, Уровень 3: сервис, интерфейс, перегрузка, protected ===\n");

        IItemService<Product> service = new ProductService();

        Console.WriteLine("-- Полиморфный вызов через IItemService<Product> --");
        Console.WriteLine($"Товаров в каталоге: {service.GetAll().Count()}");

        var found = service.GetById(2);
        Console.WriteLine($"Товар с Id=2: {found?.Name ?? "не найден"}");

        Console.WriteLine("\n-- Перегрузка методов Add --");
        
        service.Add(new Accessory(100, "Кабель USB-C", 990m, 3, 100, "Кабель"));
       
        var newProduct = service.Add("Клавиатура Keychron K2", 12990m, 3, 20);

        Console.WriteLine($"Создан через overload: Id={newProduct.Id}, {newProduct.Name}");
        Console.WriteLine($"Товаров теперь: {service.GetAll().Count()}");

        Console.WriteLine("\n-- protected-член GetDisplayName() в иерархии --");

        BaseEntity[] items =
        {
            new Laptop(200, "ASUS Zenbook 14", 89990m, 1, 5, "Intel Core Ultra 7", 16),
            new Smartphone(201, "Google Pixel 8", 74990m, 2, 8, 6.2, 4575)
        };

        foreach (var item in items)
        {
            item.PrintInfo();
            Console.WriteLine();
        }

        Console.WriteLine("=== Конец Уровня 3 ===\n");
    }
        private static void DemonstrateBlock2()
    {
        Console.WriteLine("=== Блок 2: Коллекции и LINQ ===\n");

        var service = new ProductService();

        Console.WriteLine("-- Уровень 1: List<Product> + foreach --");

        List<Product> items = service.GetAll().ToList();
        Console.WriteLine($"В каталоге товаров: {items.Count}\n");

        foreach (var p in items)
        {
            p.PrintInfo();
        }

        Console.WriteLine("\n-- Уровень 2: Dictionary по Id + TryGetValue --");

      
        int searchId = 3;
        if (TryFindItem(items, searchId, out var found))
            Console.WriteLine($"Найден: [{found!.Id}] {found.Name}");
        else
            Console.WriteLine($"Id={searchId} — не найден");

        int missingId = 999;
        if (TryFindItem(items, missingId, out var notFound))
            Console.WriteLine($"Найден: [{notFound!.Id}] {notFound.Name}");
        else
            Console.WriteLine($"Id={missingId} — не найден (безопасно обработано)");

      
        Console.WriteLine("\n-- Уровень 3: LINQ (Method Syntax) --");


        var filtered = items
            .Where(p => p.Price >= 50000m)
            .OrderByDescending(p => p.Price)
            .Select(p => new { p.Id, p.Name, p.Price, Type = p.GetType().Name })
            .ToList();

        Console.WriteLine($"\nТовары дороже 50 000 ₽ (от дорогих к дешёвым):");
        foreach (var x in filtered)
            Console.WriteLine($"  [{x.Id}] {x.Name} ({x.Type}) — {x.Price:C}");

        Console.WriteLine("\nАгрегаты:");
        Console.WriteLine($"  Count   = {items.Count}");
        Console.WriteLine($"  Sum     = {items.Sum(p => p.Price):C}");
        Console.WriteLine($"  Average = {items.Average(p => p.Price):C}");
        Console.WriteLine($"  Min     = {items.Min(p => p.Price):C}");
        Console.WriteLine($"  Max     = {items.Max(p => p.Price):C}");

        Console.WriteLine("\nГруппировка по категориям:");
        var groups = items
            .GroupBy(p => p.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                Count = g.Count(),
                AvgPrice = g.Average(p => p.Price),
                Total = g.Sum(p => p.Price * p.Stock)
            })
            .OrderBy(g => g.CategoryId);

        foreach (var g in groups)
        {
            Console.WriteLine(
                $"  Категория {g.CategoryId}: товаров {g.Count}, средняя цена {g.AvgPrice:C}, склад {g.Total:C}");
        }

        Console.WriteLine("\n=== Конец Блока 2 ===\n");
    }


    private static bool TryFindItem(List<Product> items, int id, out Product? result)
    {
        var dict = items.ToDictionary(p => p.Id, p => p);
        return dict.TryGetValue(id, out result);
    }
        private static async Task DemonstrateBlock3()
    {
        Console.WriteLine("=== Блок 3: Асинхронное программирование (async/await) ===\n");

        var service = new ProductService();

        Console.WriteLine("-- Уровень 1: async Task + await Task.Delay(1000) --");
        await service.LoadDataAsync();
        Console.WriteLine();

        Console.WriteLine("-- Уровень 2: async Task<List<Product>> + try/catch/finally --");
        var products = await service.GetAllAsync();
        Console.WriteLine($"  Получено товаров: {products.Count}\n");

        Console.WriteLine("-- Уровень 3: Task.WhenAll + CancellationToken --");

        using var cts = new CancellationTokenSource();

        var t1 = service.LoadProductByIdAsync(1, cts.Token);
        var t2 = service.LoadProductByIdAsync(2, cts.Token);
        var t3 = service.LoadProductByIdAsync(4, cts.Token);

        Console.WriteLine("  Запущены 3 параллельные задачи...");
        var results = await Task.WhenAll(t1, t2, t3);

        Console.WriteLine("  Результаты параллельной загрузки:");
        foreach (var p in results)
        {
            Console.WriteLine(p is null
                ? "    — товар не найден"
                : $"    → [{p.Id}] {p.Name}");
        }

        // Демонстрация отмены через таймер
        Console.WriteLine("\n  Демонстрация отмены (таймаут 500 мс):");
        using var cts2 = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));

        var canceledList = await service.GetAllAsync(cts2.Token);
        Console.WriteLine($"  После отмены получено товаров: {canceledList.Count}");

        Console.WriteLine("\n=== Конец Блока 3 ===\n");
    }
    }