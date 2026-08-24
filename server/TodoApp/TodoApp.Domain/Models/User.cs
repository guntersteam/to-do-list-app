namespace TodoApp.Domain.Models;

public class User : BaseEntity
{
   public string Email { get; set; }
   public string Username { get; set; }
   public string Password { get; set; }
   
   public IEnumerable<Category> Categories { get; set; }
   public IEnumerable<Task> Tasks { get; set; }
}