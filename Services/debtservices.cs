using Dapper;
using Microsoft.Data.SqlClient;
using Moneytracker.Contracts;
using Moneytracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Moneytracker.Services
{
    public class DebtService : IDebtService
    {
        private readonly IDbConnection _db;

        public DebtService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DebtModel>> GetAllDebtsAsync(string name)
        {
            var sql = "SELECT * FROM debts WHERE name = @Name";
            return await _db.QueryAsync<DebtModel>(sql, new { Name = name });
        }

        public async Task<IEnumerable<DebtModel>> GetDebtsByDateAsync(DateTime fromDate, DateTime toDate)
        {
            string query = "SELECT * FROM debts WHERE date BETWEEN @FromDate AND @ToDate";
            return await _db.QueryAsync<DebtModel>(query, new { FromDate = fromDate, ToDate = toDate });
        }

        public async Task<bool> CreateDebtAsync(DebtModel debt)
        {
            var sql = @"INSERT INTO debts (userid, date, amount, category, deadline, name, description)
                        VALUES (@UserId, @Date, @Amount, @Category, @Deadline, @Name, @Description)";
            var result = await _db.ExecuteAsync(sql, debt);
            return result > 0;
        }

        public async Task<bool> UpdateDebtAsync(DebtModel debt)
        {
            var sql = @"UPDATE debts SET 
                        date = @Date, 
                        amount = @Amount, 
                        category = @Category, 
                        deadline = @Deadline, 
                        name = @Name, 
                        description = @Description
                        WHERE debtid = @DebtId";
            var result = await _db.ExecuteAsync(sql, debt);
            return result > 0;
        }

        public async Task<bool> DeleteDebtAsync(DateTime date, string name)
        {
            var sql = "DELETE FROM debts WHERE date = @Date AND name = @Name";
            var result = await _db.ExecuteAsync(sql, new { Date = date, Name = name });
            return result > 0;
        }
    }
}