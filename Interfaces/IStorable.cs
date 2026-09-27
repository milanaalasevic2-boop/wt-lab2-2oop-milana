namespace wt_lab2_2oop_milana.Interfaces;

public interface IStorable
{
    int Id { get; }
    DateTime CreatedAt { get; }
    void PrintInfo();
}