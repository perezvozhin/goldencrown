using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.Models;
using WebApplication3.Utils;

namespace WebApplication3.Services;

public class UserService : IuserService
{
    private readonly  ApplicationDbInit _context;
    private readonly iAccountService _accountService;
    public UserService(ApplicationDbInit context, iAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    public async Task<Result> Register(string username, string password, string name)
    {   
        var exists = await _context.Users.FirstOrDefaultAsync(x => x.login == username);
        if (exists != null)
        {
            return Result.Fail("User already exists");
        }
        

        var NewUser = new User
        {
            login = username,
            password = password,
            name = name,
        };
        
        _context.Users.Add(NewUser);
        await _context.SaveChangesAsync();
        await _accountService.CreateAccount(username);
        return Result.Success();
        
    }

    public async Task<Result<string>> LoginAsync(string login, string password)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.login == login && x.password == password);
        if (user == null)
        {
            return Result<string>.Fail("User not found");
        }

        var token = Guid.NewGuid().ToString();
        var session = new Session
        {
            Userid = user.id,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
        };
        var existingSession = await _context.Sessions.FirstOrDefaultAsync(x => x.Userid == user.id);
        if (existingSession != null)
        {
            _context.Sessions.Remove(existingSession);
        }
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return Result<string>.Success(token);
    }
}

