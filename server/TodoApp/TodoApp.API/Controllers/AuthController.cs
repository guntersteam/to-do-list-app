using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Constants;
using TodoApp.Domain.Contracts.Response;
using TodoApp.Domain.Contracts.User;
using TodoApp.Domain.Interfaces.Services;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
   private readonly IAuthService _authService;

   public AuthController(IAuthService authService)
   {
      _authService = authService;
   }

   [HttpPost("sign-up")]
   public async Task<ActionResult<ApiResponse>> Register(RegisterUserRequest request, CancellationToken cancellationToken)
   {
      await _authService.Register(request, cancellationToken);
      return Ok(ApiResponse.Ok());
   }

   [HttpPost("login")]
   public async Task<ActionResult<ApiResponse>> Login(LoginUserRequest request,CancellationToken cancellationToken)
   {
      var (loginResult, refreshToken) = await _authService.Login(request, cancellationToken);
      HttpContext.Response.Cookies.Append(ApiConstants.TokenCookieName, refreshToken, new CookieOptions()
      {
         HttpOnly = true,
         Expires = DateTime.Now.AddDays(ApiConstants.CookieExpirationTime),
         SameSite = SameSiteMode.Strict
      });

      return Ok(ApiResponse.Ok(loginResult));
   }

   [HttpPost("logout")]
   public async Task<ActionResult<ApiResponse>> Logout()
   {
      HttpContext.Response.Cookies.Delete(ApiConstants.TokenCookieName);
      return Ok(ApiResponse.Ok());
   }

   [HttpPost("refresh")]
   public async Task<ActionResult<ApiResponse>> Refresh(CancellationToken cancellationToken)
   {
      var refreshToken = HttpContext.Request.Cookies[ApiConstants.TokenCookieName];

      var (refreshResult, newRefreshToken) = await _authService.RefreshTokens(refreshToken,cancellationToken);

      HttpContext.Response.Cookies.Append(ApiConstants.TokenCookieName, newRefreshToken, new CookieOptions()
      {
         HttpOnly = true,
         SameSite = SameSiteMode.Lax,
         Expires = DateTime.UtcNow.AddDays(ApiConstants.CookieExpirationTime)
      });

      return Ok(ApiResponse.Ok(refreshResult));
   }
}