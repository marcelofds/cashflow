using CashFlow.Data.Context;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data.Repositories;

public class BillsToPayRepository : Repository<BillToPay>, IBillsToPayRepository
{
    public BillsToPayRepository(CashFlowContext context) : base(context)
    {
    }
}