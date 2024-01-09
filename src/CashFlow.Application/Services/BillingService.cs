using CashFlow.Application.DataTransferObjects;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;
using Mapster;

namespace CashFlow.Application.Services;
public interface IBillingService
{
    Task<BillToPayDto?> GetBillingToPayById(int id);
    Task<BillToReceiveDto?> GetBillingToReceiveById(int id);
    Task IncludeNewBillToPay(BillToPayInsertDto bill);
    Task IncludeNewBillToReceive(BillToReceiveInsertDto bill);
}
public class BillingService : IBillingService
{
    private readonly IBillsToPayRepository _toPayRepository;
    private readonly IBillsToReceiveRepository _toReceiveRepository;

    public BillingService(IBillsToPayRepository toPayRepository, IBillsToReceiveRepository toReceiveRepository)
    {
        _toPayRepository = toPayRepository;
        _toReceiveRepository = toReceiveRepository;
    }
    public async Task<BillToPayDto?> GetBillingToPayById(int id)
    {
        return (await _toPayRepository.GetByIdAsync(id))
            .Adapt<BillToPayDto>();
    }

    public async Task IncludeNewBillToPay(BillToPayInsertDto bill)
    {
        BillToPay billToPay = bill.Adapt<BillToPay>();
        _toPayRepository.Insert(billToPay);
        await _toPayRepository.SaveAsync();
    }
    
    public async Task<BillToReceiveDto?> GetBillingToReceiveById(int id)
    {
        return (await _toReceiveRepository.GetByIdAsync(id))
            .Adapt<BillToReceiveDto>();
    }

    public async Task IncludeNewBillToReceive(BillToReceiveInsertDto bill)
    {
        BillToReceive billToReceive = bill.Adapt<BillToReceive>();
        _toReceiveRepository.Insert(billToReceive);
        await _toReceiveRepository.SaveAsync();
    }
}

