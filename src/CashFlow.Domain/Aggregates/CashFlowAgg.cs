using CashFlow.Domain.BaseDefinitions;

namespace CashFlow.Domain.Aggregates;

public class CashFlowAgg : Entity
{
    public CashFlowAgg(DateOnly date, IEnumerable<BillToPay> billsToPay, IEnumerable<BillToReceive> billsToReceive)
    {
        Date = date;
        BillsToPay = billsToPay;
        BillsToReceive = billsToReceive;
    }


    public DateOnly Date { get; set; }
    public IEnumerable<BillToPay> BillsToPay { get; set; }
    public IEnumerable<BillToReceive> BillsToReceive { get; set; }
    public decimal Value { get; set; }

    public void Consolidade()
    {
        Value = BillsToReceive.Sum(r => r.Value);
        Value -= BillsToPay.Sum(p => p.Value);
    }
}