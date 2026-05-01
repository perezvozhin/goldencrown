using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class UserService : IuserService
{
    private readonly  ApplicationDbInit _context;
    public UserService(ApplicationDbInit context)
    {
        _context = context;
    }

    public async Task<bool> Register(string username, string password, string name)
    {   
        var exists = await _context.Users.FirstOrDefaultAsync(x => x.login == username);
        if (exists != null)
        {
            return false;
        }

        if (password.Length < 6 || String.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        var NewUser = new User
        {
            login = username,
            password = password,
            name = name,
        };
        
        _context.Users.Add(NewUser);
        await _context.SaveChangesAsync();
        
        return true;
    }
}