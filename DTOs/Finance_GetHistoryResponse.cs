namespace WebApplication3.DTOs;

public class Finance_GetHistoryResponse
{
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}