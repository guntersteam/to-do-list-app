namespace TodoApp.Domain.Models;

public class TaskCategory
{
   public Guid TaskId { get; set; }
   public Guid CategoryId { get; set; }

   public Task Task { get; set; }
   public Category Category { get; set; }
}