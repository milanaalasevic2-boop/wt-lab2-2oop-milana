using wt_lab2_2oop_milana.Interfaces;

namespace wt_lab2_2oop_milana.Models;

public abstract class BaseEntity : IStorable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.Now;
    }

    protected BaseEntity(int id) : this()
    {
        Id = id;
    }

    protected virtual string GetDisplayName()
    {
        return $"Сущность #{Id}";
    }

    public abstract void PrintInfo();
}