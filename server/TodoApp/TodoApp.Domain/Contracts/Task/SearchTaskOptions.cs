using System.ComponentModel;

namespace TodoApp.Domain.Contracts.Task;

public class SearchTaskOptions
{
   public string? Title { get; set; }
   public int Page { get; set; } = 1;
   public int PageSize { get; set; } = 25;
   public bool? IsCompleted { get; set; }
   public List<Guid>? CategoryIds { get; set; }
}