namespace TodoApp.Domain.Contracts.Response;

public class ApiResponse
{
   public bool Success { get; set; }
   public object? Data { get; set; }
   public Dictionary<string, string>? Errors { get; set; }
   public string? Message { get; set; }

   public static ApiResponse Ok(object? data = null)
   {
      return new ApiResponse { Success = true, Data = data };
   }

   public static ApiResponse Fail(string? message = null,  Dictionary<string, string>? errors = null)
   {
      return new ApiResponse { Success = false, Message = message, Errors = errors};
   }
   
}