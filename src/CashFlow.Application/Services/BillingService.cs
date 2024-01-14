using CashFlow.Application.DataTransferObjects;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;
using CashFlow.Domain.Exceptions;
using Mapster;

namespace CashFlow.Application.Services;

public interface IBillingService
{
    Task<BillToPayDto?> GetBillingToPayByIdAsync(int id);
    Task<BillToReceiveDto?> GetBillingToReceiveById(int id);
    Task IncludeNewBillToPayAsync(BillToPayInsertDto bill);
    Task IncludeNewBillToReceiveAsync(BillToReceiveInsertDto bill);
    Task WriteOffBillToPayAsync(BillToPayDto bill);
    Task WriteOffBillToReceiveAsync(BillToReceiveDto bill);
    Task DeleteBillToPayAsync(int id);
    Task DeleteBillToReceiveAsync(int id);
    Task<IEnumerable<BillToPayDto>> GetAllBillToPayAsync();
    Task<IEnumerable<BillToReceiveDto>> GetAllBillToReceiveAsync();
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

    public async Task<BillToPayDto?> GetBillingToPayByIdAsync(int id)
    {
        return (await _toPayRepository.GetByIdAsync(id))
            .Adapt<BillToPayDto>();
    }

    public async Task<IEnumerable<BillToPayDto>> GetAllBillToPayAsync()
    {
        return (await _toPayRepository.GetAllAsync()).Adapt<IEnumerable<BillToPayDto>>();
    }

    public async Task<IEnumerable<BillToReceiveDto>> GetAllBillToReceiveAsync()
    {
        return (await _toReceiveRepository.GetAllAsync()).Adapt<IEnumerable<BillToReceiveDto>>();
    }

    public async Task IncludeNewBillToPayAsync(BillToPayInsertDto bill)
    {
        var billToPay = bill.Adapt<BillToPay>();
        if (billToPay.SupplierId > 0)
            billToPay.Supplier = null;
        _toPayRepository.Insert(billToPay);
        await _toPayRepository.SaveAsync();
    }

    public async Task<BillToReceiveDto?> GetBillingToReceiveById(int id)
    {
        return (await _toReceiveRepository.GetByIdAsync(id))
            .Adapt<BillToReceiveDto>();
    }

    public async Task IncludeNewBillToReceiveAsync(BillToReceiveInsertDto bill)
    {
        var billToReceive = bill.Adapt<BillToReceive>();
        if (billToReceive.CustomerId > 0)
            billToReceive.Customer = null;
        _toReceiveRepository.Insert(billToReceive);
        await _toReceiveRepository.SaveAsync();
    }

    public async Task WriteOffBillToPayAsync(BillToPayDto bill)
    {
        var billToPay = await _toPayRepository.GetByIdAsync(bill.Id);
        billToPay?.WriteOff();
        await _toPayRepository.SaveAsync();
    }

    public async Task WriteOffBillToReceiveAsync(BillToReceiveDto bill)
    {
        var billToReceive = await _toReceiveRepository.GetByIdAsync(bill.Id);
        billToReceive?.WriteOff();
        await _toReceiveRepository.SaveAsync();
    }

    public async Task DeleteBillToPayAsync(int id)
    {
        var billToPay = await _toPayRepository.GetByIdAsync(id)
                        ?? throw new CashFlowNotFoundException("Bill record not found.");
        _toPayRepository.Delete(billToPay);
        await _toPayRepository.SaveAsync();
    }

    public async Task DeleteBillToReceiveAsync(int id)
    {
        var billToReceive = await _toReceiveRepository.GetByIdAsync(id)
                            ?? throw new CashFlowNotFoundException("Bill record not found.");
        _toReceiveRepository.Delete(billToReceive);
        await _toReceiveRepository.SaveAsync();
    }
}