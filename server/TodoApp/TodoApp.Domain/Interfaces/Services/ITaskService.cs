using TodoApp.Domain.Contracts.Task;

namespace TodoApp.Domain.Interfaces.Services;

public interface ITaskService
{
   Task CreateTask(Guid userId, CreateTaskRequest request, CancellationToken cancellationToken);
   Task UpdateTask(Guid userId, UpdateTaskRequest request, CancellationToken cancellationToken);
   Task DeleteTask(Guid userId, Guid taskId,CancellationToken cancellationToken);
   Task<SearchTaskResponse> GetUserTasks(Guid userId,SearchTaskOptions searchTaskOptions, CancellationToken cancellationToken);
}