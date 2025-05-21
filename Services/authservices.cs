using Dapper;
using Microsoft.Data.SqlClient;


using MoneyTracker.Models;
using MoneyTracker.Contract;

namespace MoneyTracker.Services
{
    public class AuthService : Iauthservice
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> RegisterUserAsync(string username, string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var existingUser = await connection.QueryFirstOrDefaultAsync<user>(
                    "SELECT * FROM users WHERE Email = @Email", new { Email = email });

                if (existingUser != null)
                    return "User already exists";

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);

                await connection.ExecuteAsync(
                    "INSERT INTO users (Username, Email, Password) VALUES (@Username, @Email, @Password)",
                    new { Username = username, Email = email, Password = hashPassword });

                return "User registered successfully";
            }
        }

        public async Task<LoginResponse> LoginUserAsync(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var user = await connection.QueryFirstOrDefaultAsync<user>(
                    "SELECT * FROM Users WHERE Username = @Username", new { Username = username });

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
                {
                    return new LoginResponse
                    {
                        message = "Invalid username or password!"
                    };
                }


                return new LoginResponse
                {
                    userId = user.userId,
                    username = user.username,
                    message = "Login successful!"
                };
            }
        }
    }

    public class LoginResponse
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string message { get; set; }
    }
}
