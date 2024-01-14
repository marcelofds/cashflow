using CashFlow.Application.DataTransferObjects;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;
using Mapster;

namespace CashFlow.Application.Services;

public class CashFlowService : ICashFlowService
{
    private readonly IBillsToPayRepository _toPayRepository;
    private readonly IBillsToReceiveRepository _toReceiveRepository;

    public CashFlowService(IBillsToPayRepository toPayRepository, IBillsToReceiveRepository toReceiveRepository)
    {
        _toPayRepository = toPayRepository;
        _toReceiveRepository = toReceiveRepository;
    }

    public async Task<CashFlowAggDto> ConsolidateAsync(DateOnly date)
    {
        var toPay = await _toPayRepository.GetAllByExpressionAsync(p => p.ExpirationDate == date);
        var toReceive = await _toReceiveRepository.GetAllByExpressionAsync(r => r.ExpirationDate == date);
        var cashFlow = new CashFlowAgg(date, toPay, toReceive);
        cashFlow.Consolidade();
        return new CashFlowAggDto
        {
            Date = cashFlow.Date,
            BillsToPay = cashFlow.BillsToPay.Adapt<List<BillToPayDto>>(),
            BillsToReceive = cashFlow.BillsToReceive.Adapt<List<BillToReceiveDto>>(),
            Value = cashFlow.Value
        };
    }
}

public interface ICashFlowService
{
    Task<CashFlowAggDto> ConsolidateAsync(DateOnly date);
}