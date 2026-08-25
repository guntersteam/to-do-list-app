using System.Security.Claims;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.Interfaces.Helpers;

public interface IJwtProvider
{
   string GenerateAccessToken(User user);
   string GenerateRefreshToken(User user);
   ClaimsPrincipal GetPrincipals(string refreshToken);
}