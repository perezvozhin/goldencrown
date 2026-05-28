using WebApplication3.DTOs;
using WebApplication3.Utils;

namespace WebApplication3.Services;

public interface IFinanceService
{
    Task<Result<decimal>> GetBalance(string token);
    Task<Result<decimal>> DepositAsync(string token, decimal amount);
    Task<Result<decimal>> TransferAsync(string token, string to, decimal amount);

    Task<Result<IEnumerable<Finance_GetHistoryResponse>>> TransactionsAsync(string token, DateTime? dateFrom,
        DateTime? dateTo, int skip, int take);
}