using CashFlow.Domain.Aggregates;

namespace CashFlow.Application.DataTransferObjects;

public class BillToReceiveInsertDto
{
    public Decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public CustomerDto Customer { get; set; }
}