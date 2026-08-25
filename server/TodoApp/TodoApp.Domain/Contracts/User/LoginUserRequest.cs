using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Contracts.User;

public class LoginUserRequest
{
   [Required]
   [EmailAddress]
   public string Email { get; set; }
   
   [Required]
   public string Password { get; set; }
}