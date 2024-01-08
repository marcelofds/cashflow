using CashFlow.Domain.Aggregates;

namespace CashFlow.Application.DataTransferObjects;

public class BillToPayInsertDto
{
    public Decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public Supplier Supplier { get; set; }
}