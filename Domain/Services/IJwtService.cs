
namespace Domain.Services
{
    public interface IJwtService
    {
        string CreateToken(string username);
        string GetUsername(string token);
    }
}
