using System.ComponentModel.DataAnnotations;
using TodoApp.Domain.Constants;

namespace TodoApp.Domain.Contracts.Task;

public class CreateTaskRequest
{
   [Required]
   [MaxLength(ValidationConstants.Task.MaximumTaskTitleLenght,ErrorMessage = "{0} can't be longer than {1} characters")]
   public string Title { get; set; }
   
   [MaxLength(ValidationConstants.Task.MaximumTaskNoteLenght,ErrorMessage = "{0} can't be longer than {1} characters")]
   public string? Note { get; set; }
   public DateTime? DueTime { get; set; }
   public List<Guid>? CategoryIds { get; set; }
}