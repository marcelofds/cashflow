using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.WebApi.Controllers;

[Produces("application/json")]
[Route("api/billings-to-receive")]
[Authorize]
public class BillingToReceiveController : ControllerBase
{
    private readonly ILogger<BillingToReceiveController> _logger;
    private readonly IBillingService _service;

    public BillingToReceiveController(IBillingService service, ILogger<BillingToReceiveController> logger)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillToReceiveDto>>> GetAllAsync()
    {
        return (await _service.GetAllBillToReceiveAsync()).ToList();
    }

    /// <summary>
    ///     Find out a bill title by id
    /// </summary>
    /// <param name="id">Id of bill title to fetch the record</param>
    /// <returns>Null or the bill title that match with Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BillToReceiveDto?>> GetBillToReceiveByIdAsync(int id)
    {
        return await _service.GetBillingToReceiveById(id);
    }

    /// <summary>
    ///     Write off a bill title
    /// </summary>
    /// <param name="bill">the instance to manage</param>
    /// <returns>No content</returns>
    [HttpPut]
    public async Task<IActionResult> WriteOffAsync([FromBody] BillToReceiveDto bill)
    {
        await _service.WriteOffBillToReceiveAsync(bill);
        _logger.LogInformation("The user ${User} has been wrote off the bill to receive title ${@Bill}",
            HttpContext?.User?.Identity?.Name, bill);
        return NoContent();
    }

    /// <summary>
    ///     Delete a register that matches with id.
    /// </summary>
    /// <param name="id">Id of bill title to fetch the record</param>
    /// <returns>No content</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteBillToReceiveAsync(id);
        _logger.LogInformation("The user ${User} has been deleted the bill to receive title ${Id}",
            HttpContext?.User?.Identity?.Name, id);
        return NoContent();
    }

    /// <summary>
    ///     Insert new bill to receive.
    /// </summary>
    /// <param name="bill"> bill title to insert</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<ActionResult<BillToPayDto?>> AddNewAsync([FromBody] BillToReceiveInsertDto bill)
    {
        await _service.IncludeNewBillToReceiveAsync(bill);
        _logger.LogInformation("The user ${User} has been added the bill to receive title ${@Bill}",
            HttpContext?.User?.Identity?.Name, bill);
        return NoContent();
    }
}