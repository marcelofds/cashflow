using CashFlow.Application.Services;
using CashFlow.Domain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

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
    
    [HttpGet("consolidate")]
    public async Task<ActionResult<CashFlowAgg>> Consolidate(DateOnly date)
    {
        return Ok(await _service.Consolidate(date));
    }
}