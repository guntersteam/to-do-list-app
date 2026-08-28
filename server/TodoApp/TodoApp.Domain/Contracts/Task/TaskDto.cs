using TodoApp.Domain.Contracts.Category;

namespace TodoApp.Domain.Contracts.Task;

public class TaskDto
{
   public Guid Id { get; set; }
   public string Title { get; set; }
   public string? Note { get; set; }
   public DateTime CreatedAt { get; set; }
   public DateTime? DueTime { get; set; }
   public bool IsCompleted { get; set; }
   public List<CategoryDto> TaskCategories { get; set; }
}