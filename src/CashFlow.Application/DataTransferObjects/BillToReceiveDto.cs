namespace CashFlow.Application.DataTransferObjects;

public class BillToReceiveDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public DateOnly PaymentDate { get; set; }
    public CustomerDto Customer { get; set; }
}