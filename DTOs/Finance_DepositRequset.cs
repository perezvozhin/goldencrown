using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.DTOs;

public class Finance_DepositRequset
{
    [FromQuery]
    [Required]
    public string Token { get; set; }
    public decimal Amount { get; set; }
}