using CashFlow.Domain.BaseDefinitions;
using CashFlow.Domain.Exceptions;

namespace CashFlow.Domain.Aggregates;

public class BillToPay : Entity
{
    public BillToPay()
    {
        
    }
    public BillToPay(decimal value, DateOnly expirationDate, Supplier supplier)
    {
        Value = value;
        ExpirationDate = expirationDate;
        Supplier = supplier;
    }

    public Decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public Supplier Supplier { get; set; }
    public bool IsExpired() => ExpirationDate > DateOnly.FromDateTime(DateTime.Today);

    public void WriteOff()
    {
        if (IsExpired())
        {
            throw new CashFlowInvalidOperationException("The system can't write off an expired title");
        }
        PaymentDate = DateOnly.FromDateTime(DateTime.Today);
    }
    
}