namespace WebApplication3.Services;

public interface IuserService
{
    Task<bool> Register(string username, string password, string name);
}