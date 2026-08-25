namespace TodoApp.Domain.Contracts.User;

public class LoginUserResponse
{
   public string AccessToken { get; set; }
   public UserDto User { get; set; }
}