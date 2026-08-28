namespace TodoApp.Domain.Models;

public class Task : BaseEntity
{
   public string Title { get; set; }
   public string? Note { get; set; }
   public bool IsCompleted { get; set; }
   public DateTime? DueTime { get; set; }
   
   public Guid UserId { get; set; }
   public User User { get; set; }
   public List<TaskCategory> TaskCategories { get; set; }
}