namespace WebApplication3.Models;

public class Session
{
    public int Userid { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}