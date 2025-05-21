using MoneyTracker.Services;
namespace MoneyTracker.Contract
{
    public interface Iauthservice
    {
        Task<string> RegisterUserAsync(string username, string email, string password);
        Task<LoginResponse> LoginUserAsync(string username, string password);
    }
}