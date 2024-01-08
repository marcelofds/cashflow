
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;

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
    public async Task<CashFlowAgg> Consolidate(DateOnly date)
    {
        var toPay = await _toPayRepository.GetAllAsync();
        var toReceive = await _toReceiveRepository.GetAllAsync();
        var cashFlow = new CashFlowAgg(date, toPay, toReceive);
        cashFlow.Consolidade();
        return cashFlow;
    }
}

public interface ICashFlowService
{
    Task<CashFlowAgg> Consolidate(DateOnly date);
}