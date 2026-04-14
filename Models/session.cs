namespace WebApplication3.Models;

public class session
{
    public int userid { get; set; }
    public string token { get; set; }
    public DateTime expiresAt { get; set; }
}