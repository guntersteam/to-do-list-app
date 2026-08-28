using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Contracts.Task;

public class UpdateTaskRequest
{
   [Required] 
   public Guid TaskId { get; set; }
   
   [Required]
   public string Title { get; set; }
   public string? Note { get; set; }
   public DateTime? DueTime { get; set; }
   public bool? IsCompleted { get; set; }
   public List<Guid>? CategoryIds { get; set; }
}