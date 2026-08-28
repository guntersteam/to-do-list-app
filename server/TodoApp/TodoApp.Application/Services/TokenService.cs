using TodoApp.Domain.Interfaces.Helpers;
using TodoApp.Domain.Interfaces.Services;
using TodoApp.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoApp.Application.Services;

public class TokenService(IJwtProvider jwtProvider) : ITokenService
{
   public Task<Tuple<string, string>> GenerateTokens(User user)
   {
      var accessToken = jwtProvider.GenerateAccessToken(user);
      var refreshToken = jwtProvider.GenerateRefreshToken(user);

      return Task.FromResult(Tuple.Create(accessToken, refreshToken));
   }

   public (bool IsValid, string UserId) IsTokenValid(string refreshToken)
   {
      var principal = jwtProvider.GetPrincipals(refreshToken);
      
      if (principal == null || principal.Claims.All(c => c.Type == "Id"))
      {
         return (false, string.Empty);
      }

      var userId = principal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

      if (string.IsNullOrEmpty(userId))
      {
         return (false, string.Empty);
      }

      return (true, userId);
   }
   
}