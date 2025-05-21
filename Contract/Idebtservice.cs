using Moneytracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moneytracker.Contracts
{
    public interface IDebtService
    {
        Task<IEnumerable<DebtModel>> GetAllDebtsAsync(string name);
        Task<IEnumerable<DebtModel>> GetDebtsByDateAsync(DateTime fromDate, DateTime toDate);
        Task<bool> CreateDebtAsync(DebtModel debt);
        Task<bool> UpdateDebtAsync(DebtModel debt);
        Task<bool> DeleteDebtAsync(DateTime date, string name);
    }
}