using CashFlow.Domain.BaseDefinitions;
using CashFlow.Domain.Exceptions;

namespace CashFlow.Domain.Aggregates;

public class BillToReceive : Entity
{
    public BillToReceive()
    {
    }

    public BillToReceive(decimal value, DateOnly expirationDate, Customer customer)
    {
        Value = value;
        ExpirationDate = expirationDate;
        if (customer.Id == 0)
            Customer = customer;
        else
            CustomerId = customer.Id;
    }

    public decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public bool IsExpired()
    {
        return ExpirationDate > DateOnly.FromDateTime(DateTime.Today);
    }

    public void WriteOff()
    {
        if (IsExpired()) throw new CashFlowInvalidOperationException("The system can't write off an expired title");
        PaymentDate = DateOnly.FromDateTime(DateTime.Today);
    }
}