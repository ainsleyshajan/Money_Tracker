using Dapper;
using Microsoft.Data.SqlClient;


using MoneyTracker.Models;
using MoneyTracker.Contract;

namespace MoneyTracker.Services
{
    public class ExpenseService : Iexpenseservice
    {
        private readonly string _connectionString;

        public ExpenseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> AddExpense(int userid,DateTime date,decimal amount,string category,string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var add = await _dbConnection.ExecuteAsync("Insert into expenses(userid,date,amount,category,description) values(@userid,@date,@amount,@category,@description)",
                new { userid = userid, date,amount,category,description });

                return add > 0 ? "Expense added" : "Failed to add";
            }
        }

        public async Task<string> AddExpense(int userid, DateTime date, decimal amount, string category, string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var add = await _dbConnection.ExecuteAsync("Insert into expenses(userid,date,amount,category,description) values(@userid,@date,@amount,@category,@description)",
                new { userid = userid, date, amount, category, description });

                return add > 0 ? "Expense added" : "Failed to add";
            }
        }

    }
