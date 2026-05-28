using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.DTOs;
using WebApplication3.Models;
using WebApplication3.Utils;

namespace WebApplication3.Services;

public class FinanceService : IFinanceService
{
    private readonly  ApplicationDbInit _context;
    public FinanceService(ApplicationDbInit context)
    {
        _context = context;
    }

    
    public async Task<Result<decimal>>  GetBalance(string token)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
        if (session == null)
        {
            return Result<decimal>.Fail("Unauthorized");
        }
        var user  = await _context.Users.FirstOrDefaultAsync(u => u.id == session.Userid);
        if (user == null)
        {
            return Result<decimal>.Fail("User not found");
        }
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.userId == user.id);
        if (account == null)
        {
            return Result<decimal>.Fail("Account not found");
        }
        return Result<decimal>.Success(account!.balance);
    }

    public async Task<Result<decimal>> DepositAsync(string token, decimal amount)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
        if (session == null)
        {
            return Result<decimal>.Fail("Unauthorized");
        }
        var account = await _context.Accounts.FirstOrDefaultAsync(x => x.userId == session.Userid);
        if (account == null)
        {
            return Result<decimal>.Fail("Account not found");
        }
        account.balance += amount;

        var transaction = new Transaction
        {
            SenderAccountId = account.id,
            ReceiverAccountId = account.id,
            Date = DateTime.Now,
            Amount = amount,
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return Result<decimal>.Success(account.balance);
    }

    public async Task<Result<decimal>> TransferAsync(string token, string to, decimal amount)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
        if (session == null)
        {
            return Result<decimal>.Fail("Unauthorized");
        }
        var senderacc = await _context.Accounts.FirstOrDefaultAsync(x => x.id == session.Userid);
        if (senderacc == null)
        {
            return Result<decimal>.Fail("Account not found");
        }
        senderacc.balance -= amount;
        var recieverlogin = await _context.Users.FirstOrDefaultAsync(x => x.login == to);
        if (recieverlogin == null)
        {
            return Result<decimal>.Fail("Account not found");
        }

        var recieveracc = await _context.Accounts.FirstOrDefaultAsync(x => x.userId == recieverlogin.id);
        if (recieveracc == null)
        {
            return Result<decimal>.Fail("Account not found");
        }
        recieveracc.balance += amount;

        var transaction = new Transaction{
            SenderAccountId = senderacc.id,
            ReceiverAccountId = recieveracc.id,
            Date = DateTime.Now,
            Amount = amount
            };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return Result<decimal>.Success(senderacc.balance);

    }

    public async Task<Result<IEnumerable<Finance_GetHistoryResponse>>> TransactionsAsync(string token, DateTime? dateFrom, DateTime? dateTo, int skip, int take)
    {
        
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
        if (session == null)
        {
            return Result<IEnumerable<Finance_GetHistoryResponse>>.Fail("Unauthorized");
        }
        var user = await _context.Users.FirstOrDefaultAsync(x => x.id == session.Userid);
        if (user == null)
        {
            return Result<IEnumerable<Finance_GetHistoryResponse>>.Fail("User not found");
        }
        var account = await _context.Accounts.FirstOrDefaultAsync(x => x.userId == session.Userid);
        if (account == null)
        {
            return Result<IEnumerable<Finance_GetHistoryResponse>>.Fail("Account not found");
        }

        var transactions = _context.Transactions.Where(x => x.SenderAccountId == account.id
                                                            || x.ReceiverAccountId == account.id);

        if (dateFrom != null)
        {
            transactions = transactions.Where(x => x.Date >= dateFrom.Value);
        }
        if (dateTo != null)
        {
            transactions = transactions.Where(x => x.Date <= dateTo.Value);
        }

        transactions = transactions.Skip(skip).Take(take);
        var transactions_result = await transactions.ToListAsync();

        var result = new List<Finance_GetHistoryResponse>();
        
        var allsenders = transactions.Select(x => x.SenderAccountId);
        var allrecievers = transactions.Select(x => x.ReceiverAccountId);

        var allaccounts = allsenders.ToHashSet();
        foreach (var reciever in allrecievers)
        {
            allaccounts.Add(reciever);
        }
        
        var names = await _context.Accounts.Where(x => allaccounts.Contains(x.id)).Join(_context.Users, acc => acc.userId, u => u.id,
            (acc, u)
                => new 
            {
                Name = u.name,
                AccId = acc.id
            }).ToDictionaryAsync(x => x.AccId);
        
        foreach (var transaction in transactions_result)
        {
            
            
           result.Add(new Finance_GetHistoryResponse
           {
               SenderName = transaction.SenderName,
               ReceiverName = transaction.ReceiverName,
               Amount = transaction.Amount,
               Date = transaction.Date
           });
        }

        return result;
    }

}