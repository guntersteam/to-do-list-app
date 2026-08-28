using TodoApp.Domain.Models;

namespace TodoApp.Domain.Interfaces.Services;

public interface ITokenService
{
   Task<Tuple<string, string>> GenerateTokens(User user);
   (bool IsValid,string UserId) IsTokenValid(string refreshToken);
}