using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using CashFlow.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;
[Produces("application/json")]
[Route("api/billings")]
public class BillingController : ControllerBase
{
    private readonly IBillingService _service;

    public BillingController(IBillingService service)
    {
        _service = service;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBillToPayById(int id)
    {
        return Ok(await _service.GetBillingToPayById(id));
    }

    [HttpPost("bill-to-pay")]
    public async Task<IActionResult> AddNewBillToPay([FromBody]BillToPayInsertDto bill)
    {
        await _service.IncludeNewBillToPay(bill);
        return NoContent();
    }
    
    [HttpPost("bill-to-receive")]
    public async Task<IActionResult> AddNewBillToPay([FromBody]BillToReceiveInsertDto bill)
    {
        await _service.IncludeNewBillToReceive(bill);
        return NoContent();
    }
}