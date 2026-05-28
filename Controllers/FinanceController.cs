using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinanceController : Controller
{
    private readonly IFinanceService _financeService;

    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalanceAsync([FromHeader]string token)
    {

        var result = await _financeService.GetBalance(token);
        
        if (result.isSuccess)
        {
            return Ok(new Finance_BalanceResponse{ Balance = result.value });
        }
        return NotFound();
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync([FromBody] Finance_DepositRequset request)
    {
        var result = await _financeService.DepositAsync(request.Token, request.Amount);
        if (result.isSuccess)
        {
            return Ok(new Finance_BalanceResponse { Balance = result.value });
        }
        return BadRequest();
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferAsync([FromBody] Finance_TransferRequest request)
    {
        var result = await _financeService.TransferAsync(request.Token, request.ReceiverLogin, request.Amount);
        if (result.isSuccess)
        {
            return Ok(new Finance_BalanceResponse { Balance = result.value });
        }
        return BadRequest();
    }

    [HttpGet("transaction")]
    public async Task<IActionResult> TransactionsAsync([FromBody] Finance_TransactionHistoryReq request)
    {
        var result = await _financeService.TransactionsAsync(request.Token, request.From, request.To, request.Limit, request.Offset);
        if (result.isSuccess)
        {
            return Ok(result.value);
        }
        return BadRequest();
    }
}