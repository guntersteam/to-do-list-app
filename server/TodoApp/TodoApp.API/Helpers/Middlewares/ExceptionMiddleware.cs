using TodoApp.Domain.Contracts.Exception;
using TodoApp.Domain.Contracts.Response;

namespace TodoApp.API.Helpers.Middlewares;

public class ExceptionMiddleware
{
   private readonly RequestDelegate _next;
   private readonly ILogger<ExceptionMiddleware> _logger;

   public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
   {
      _next = next;
      _logger = logger;
   }

   public async Task Invoke(HttpContext context)
   {
      try
      {
         await _next(context);
      }
      catch (ApiException ex)
      {
         _logger.LogWarning(
            "API Exception (StatusCode: {StatusCode}) Message: {Message} Errors: {@Errors}",
            ex.StatusCode,
            ex.Message,
            ex.Errors
         );

         var errorResponse = ApiResponse.Fail(
            message: ex.Message,
            errors: ex.Errors ?? new Dictionary<string, string>()
         );

         context.Response.StatusCode = ex.StatusCode;
         context.Response.ContentType = "application/json";

         await context.Response.WriteAsJsonAsync(errorResponse);
      }
      catch (Exception ex)
      {
         _logger.LogError(ex, "Unhandled exception");

         var errorResponse = ApiResponse.Fail(message: "Internal server error");

         context.Response.StatusCode = 500;
         context.Response.ContentType = "application/json";

         await context.Response.WriteAsJsonAsync(errorResponse);
      }
   }
}