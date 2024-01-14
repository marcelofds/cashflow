using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.WebApi.Controllers;

/// <summary>
///     Endpoint to manage bills to pay
/// </summary>
[Produces("application/json")]
[Route("api/billings-to-pay")]
[Authorize]
public class BillingToPayController : ControllerBase
{
    private readonly ILogger<BillingToPayController> _logger;

    private readonly IBillingService _service;

    public BillingToPayController(IBillingService service, ILogger<BillingToPayController> logger)
    {
        _logger = logger;
        _service = service;
    }


    [HttpGet]
    public async Task<ActionResult<List<BillToPayDto>>> GetAllAsync()
    {
        return (await _service.GetAllBillToPayAsync()).ToList();
    }

    /// <summary>
    ///     Finds out a bill to pay that fetches with {id}
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Null if it not found or the bill title fetched</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BillToPayDto?>> GetBillToPayByIdAsync(int id)
    {
        return await _service.GetBillingToPayByIdAsync(id);
    }

    /// <summary>
    ///     Write off a bill title
    /// </summary>
    /// <param name="bill">the instance to manage</param>
    /// <returns>No content</returns>
    [HttpPut]
    public async Task<IActionResult> WriteOffAsync([FromBody] BillToPayDto bill)
    {
        await _service.WriteOffBillToPayAsync(bill);
        _logger.LogInformation("The user ${User} has been wrote off the bill to pay title ${@Bill}",
            HttpContext?.User?.Identity?.Name, bill);
        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteBillToPayAsync(id);
        _logger.LogInformation("The user ${User} has been deleted the bill to pay title ${Id}",
            HttpContext?.User?.Identity?.Name, id);
        return NoContent();
    }

    /// <summary>
    ///     Insert new bill to pay.
    /// </summary>
    /// <param name="bill"> bill title to insert</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> AddNewAsync([FromBody] BillToPayInsertDto bill)
    {
        await _service.IncludeNewBillToPayAsync(bill);
        _logger.LogInformation("The user ${User} has been added the bill to pay title ${@Bill}",
            HttpContext?.User?.Identity?.Name, bill);
        return NoContent();
    }
}