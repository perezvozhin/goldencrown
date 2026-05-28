namespace WebApplication3.Models;

public class Transaction
{
    public  int id { get; set; }
    public int SenderAccountId { get; set; }
    public int ReceiverAccountId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    
}