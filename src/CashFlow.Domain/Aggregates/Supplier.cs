using CashFlow.Domain.BaseDefinitions;

namespace CashFlow.Domain.Aggregates;

public class Supplier : Entity
{
    public Supplier()
    {
    }

    public Supplier(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}