namespace WebApplication3.Models;

public class Sessions
{
    public int userid { get; set; }
    public string token { get; set; }
    public DateTime expiresAt { get; set; }
}