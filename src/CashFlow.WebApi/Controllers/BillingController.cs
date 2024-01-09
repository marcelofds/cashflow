using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
[Produces("application/json")]
[Route("api/billings")]
[Authorize]
public class BillingController : ControllerBase
{
    private readonly ILogger<BillingController> _logger;
    //private readonly ILoggedInUser _loggedInUser;
    private readonly IBillingService _service;

    public BillingController(IBillingService service)
    {
        _service = service;
    }
    [HttpGet("{id}/bill-to-pay")]
    public async Task<IActionResult> GetBillToPayById(int id)
    {
        return Ok(await _service.GetBillingToPayById(id));
    }
    
    [HttpGet("{id}/bill-to-receive")]
    public async Task<IActionResult> GetBillToReceiveById(int id)
    {
        return Ok(await _service.GetBillingToReceiveById(id));
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