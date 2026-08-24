using Task = TodoApp.Domain.Models.Task;

namespace TodoApp.Domain.Interfaces.Repositories;

public interface ITaskRepository : IRepository<Task>
{
   
}