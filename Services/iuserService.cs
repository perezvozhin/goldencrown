using WebApplication3.Utils;

namespace WebApplication3.Services;

public interface IuserService
{
    Task<Result> Register(string username, string password, string name);
    Task<Result<string>> LoginAsync(string login, string password);
}