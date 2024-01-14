namespace CashFlow.Application.DataTransferObjects;

public class BillToPayInsertDto
{
    public decimal Value { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public SupplierDto Supplier { get; set; }
}