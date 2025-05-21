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

        public async Task<string> UpdateExpense(int userid,int expenseid, DateTime date, decimal amount, string category, string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE expenses SET " +
                      "date = @date, " +
                      "amount = @amount, " +
                      "category = @category, " +
                      "description = @description " +
                      "WHERE userid = @userid AND expenseid = @expenseid";

                var update = await _dbConnection.ExecuteAsync(sql, new
                {
                    userid = userid,
                    expenseid = expenseid,
                    date = date,
                    amount = amount,
                    category = category,
                    description = description
                });

                return update > 0 ? "Updated" : "Failed to update";
            }
        }

        public async Task<string> Delete(int expenseid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var delete = await _dbConnection.ExecuteAsync(
                "DELETE FROM expense WHERE expenseid = @expenseid",
                new { expenseid = expenseid }
            );

                return delete > 0 ? "Deleted successfully" : "Failed to delete";
            }
        }

        public async Task<Tasks> GetByexpenseIdAsync(int expenseid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var task = await _dbConnection.QueryFirstOrDefaultAsync<Tasks>(
                "SELECT * FROM expenses WHERE expenseid = @expenseid", new { expenseid = expenseid });

                if (task == null)
                    throw new Exception("Expense not found");

                return task;
            }
        }
    }
