using CashFlow.Data.Context;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;

namespace CashFlow.Data.Repositories;

public class BillsToReceiveRepository : Repository<BillToReceive>, IBillsToReceiveRepository
{
    public BillsToReceiveRepository(CashFlowContext context) : base(context)
    {
    }
}