using CashFlow.Domain.Aggregates;

namespace CashFlow.Application.DataTransferObjects;

public class BillToReceiveDto
{
    public int Id { get; set; }
    public Decimal Value { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public Customer CustomerDto { get; set; }
}