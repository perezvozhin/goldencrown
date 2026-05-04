using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class AccountService : iAccountService
{
    private readonly  ApplicationDbInit _context;
    public AccountService(ApplicationDbInit context)
    {
        _context = context;
    }

    public async Task CreateAccount(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.login == login);
        if (user == null)
        {
            throw new InvalidOperationException($"User not found with login: {login}");
        }
        var Account = new Account
        {
            balance = 0,
            userId = user.id,
        };
        _context.Accounts.Add(Account);
        await _context.SaveChangesAsync();
    }
}