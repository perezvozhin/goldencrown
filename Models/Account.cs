namespace WebApplication3.Models;

public class Account
{
    public int id { get; set; }
    public int userId { get; set; }
    public decimal balance { get; set; }
    public User user { get; set; }
}