using wt_lab2_2oop_milana.Models;

namespace wt_lab2_2oop_milana.Interfaces;

public interface IItemService<T> where T : BaseEntity
{
    void Add(T item);
    Product Add(string name, decimal price, int categoryId, int stock); // перегрузка
    bool Remove(int id);
    T? GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate);
}