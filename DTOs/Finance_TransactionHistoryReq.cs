using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.DTOs;

public class Finance_TransactionHistoryReq
{
    [FromQuery]
    [Required]
    public string Token { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}