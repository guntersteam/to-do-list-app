using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
   [SwaggerOperation("Sign up user in system")]
   public async Task<ActionResult<ApiResponse>> Register(RegisterUserRequest request, CancellationToken cancellationToken)
   {
      await _authService.Register(request, cancellationToken);
      return Ok(ApiResponse.Ok());
   }

   [HttpPost("sign-in")]
   [SwaggerOperation("Sign in user in system")]
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
   [SwaggerOperation("Logout user in system")]
   public async Task<ActionResult<ApiResponse>> Logout()
   {
      HttpContext.Response.Cookies.Delete(ApiConstants.TokenCookieName);
      return Ok(ApiResponse.Ok());
   }

   [HttpPost("refresh")]
   [SwaggerOperation("Refresh user tokens")]
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