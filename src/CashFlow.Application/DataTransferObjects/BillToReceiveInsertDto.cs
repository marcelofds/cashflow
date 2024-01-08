using CashFlow.Domain.Aggregates;

namespace CashFlow.Application.DataTransferObjects;

public class BillToReceiveInsertDto
{
    public Decimal Value { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Customer CustomerDto { get; set; }
}