namespace WebApplication3.Models;

public class Transaction
{
    public  int id { get; set; }
    public int senderAccountId { get; set; }
    public int receiverAccountId { get; set; }
    public DateTime date { get; set; }
    public decimal amount { get; set; }
    
}