namespace CashFlow.Application.DataTransferObjects;

public class BillToPayDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public SupplierDto Supplier { get; set; }
}