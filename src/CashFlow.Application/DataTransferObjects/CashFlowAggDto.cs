
using CashFlow.Domain.Aggregates;

namespace CashFlow.Application.DataTransferObjects;

public class CashFlowAggDto
{
    public DateOnly Date { get; set; }
    public IEnumerable<BillToPay> BillsToPay { get; set; }
    public IEnumerable<BillToReceive> BillsToReceive { get; set; }
    public decimal Value { get; set; }
}