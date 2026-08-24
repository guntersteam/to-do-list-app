using TodoApp.Domain.Interfaces.Repositories;
using Task = TodoApp.Domain.Models.Task;

namespace TodoApp.Persistence.Repositories;

public class TaskRepository : GenericRepository<Task>, ITaskRepository
{
   public TaskRepository(AppDbContext context) : base(context)
   {
   }
}