namespace CashFlow.Application.DataTransferObjects;

public class CashFlowAggDto
{
    public DateOnly Date { get; set; }
    public IEnumerable<BillToPayDto> BillsToPay { get; set; }
    public IEnumerable<BillToReceiveDto> BillsToReceive { get; set; }
    public decimal Value { get; set; }
}