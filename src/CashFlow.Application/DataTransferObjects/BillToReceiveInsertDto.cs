namespace CashFlow.Application.DataTransferObjects;

public class BillToReceiveInsertDto
{
    public decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public CustomerDto Customer { get; set; }
}