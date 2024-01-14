using CashFlow.Domain.BaseDefinitions;

namespace CashFlow.Domain.Aggregates;

public class Customer : Entity
{
    public Customer()
    {
    }

    public Customer(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}