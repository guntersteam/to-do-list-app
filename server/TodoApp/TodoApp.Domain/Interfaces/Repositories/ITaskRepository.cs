using TodoApp.Domain.Contracts.Task;
using DomainTask = TodoApp.Domain.Models.Task;

namespace TodoApp.Domain.Interfaces.Repositories;

public interface ITaskRepository : IRepository<DomainTask>
{
   Task CreateCategoryTasks(List<Guid> categoryIds, Guid taskId, CancellationToken cancellationToken);
   Task UpdateCategoryTasks(List<Guid> categoryIds, Guid taskId, CancellationToken cancellationToken);
   Task<(List<DomainTask> Items, int TotalCount)> GetPagedAndFilteredAsync(Guid userId, SearchTaskOptions options);
}