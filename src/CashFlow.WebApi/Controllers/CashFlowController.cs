using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.WebApi.Controllers;

/// <summary>
///     End point to manage the cash flow content
/// </summary>
[Produces("application/json")]
[Route("api/cashflows")]
[Authorize]
public class CashFlowController : ControllerBase
{
    private readonly ICashFlowService _service;

    public CashFlowController(ICashFlowService service)
    {
        _service = service;
    }

    /// <summary>
    ///     Consolidate all bills titles to defined date
    /// </summary>
    /// <param name="date">The day date to consolidate</param>
    /// <returns>The consolidated View Model containing all titles for the date and summarized value</returns>
    [HttpGet("consolidate")]
    public async Task<ActionResult<CashFlowAggDto>> ConsolidateAsync(DateOnly date)
    {
        return await _service.ConsolidateAsync(date);
    }
}