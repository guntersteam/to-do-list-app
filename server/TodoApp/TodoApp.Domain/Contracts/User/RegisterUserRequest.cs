using System.ComponentModel.DataAnnotations;
using TodoApp.Domain.Constants;

namespace TodoApp.Domain.Contracts.User;

public class RegisterUserRequest
{
   [Required]
   [EmailAddress]
   public string Email { get; set; }
   
   [Required]
   [Length(ValidationConstants.User.MinimumPasswordLength, ValidationConstants.User.MaximumPasswordLength, ErrorMessage = "{0} must be between {1} and {2} characters long.")]
   public string Password { get; set; }
   
   [Required]
   [Length(ValidationConstants.User.MinimumUsernameLength, ValidationConstants.User.MaximumUsernameLength, ErrorMessage = "{0} must be between {1} and {2} characters long.")]
   public string Username { get; set; }
}