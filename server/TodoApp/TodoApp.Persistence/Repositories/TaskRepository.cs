using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Contracts.Task;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Models;
using Task = System.Threading.Tasks.Task;
using DomainTask = TodoApp.Domain.Models.Task;

namespace TodoApp.Persistence.Repositories;

public class TaskRepository : GenericRepository<DomainTask>, ITaskRepository
{
   public TaskRepository(AppDbContext context) : base(context)
   {
   }

   public async Task CreateCategoryTasks(List<Guid> categoryIds, Guid taskId, CancellationToken cancellationToken)
   {
      if (categoryIds == null || categoryIds.Count == 0)
         return;
      
      var categoryTasks = categoryIds.Select(categoryId => new TaskCategory
      {
         CategoryId = categoryId,
         TaskId = taskId
      }).ToList();
      
      await _context.TaskCategories.AddRangeAsync(categoryTasks, cancellationToken);
   }

   public async Task UpdateCategoryTasks(List<Guid> categoryIds, Guid taskId, CancellationToken cancellationToken)
   {
      categoryIds ??= [];
      
      var existingCategories = await _context.TaskCategories
         .Where(tc => tc.TaskId == taskId)
         .ToListAsync(cancellationToken);
      
      var toDelete = existingCategories
         .Where( tc => !categoryIds.Contains(tc.CategoryId))
         .ToList();
      
      var existingCategoryIds = existingCategories.Select(tc => tc.CategoryId).ToHashSet();
      
      var toAdd = categoryIds
         .Where(id => !existingCategoryIds.Contains(id))
         .Select(categoryId => new TaskCategory
         {
            CategoryId = categoryId,
            TaskId = taskId
         })
         .ToList();

      if (toDelete.Count != 0)
      {
         _context.TaskCategories.RemoveRange(toDelete);
      }

      if (toAdd.Count != 0)
      {
         _context.TaskCategories.AddRange(toAdd);
      }
   }

   public async Task<(List<DomainTask> Items, int TotalCount)> GetPagedAndFilteredAsync(Guid userId, SearchTaskOptions options)
   {
      var query = _context.Tasks
         .Where(t => t.UserId == userId)
         .AsQueryable();

      if (!string.IsNullOrEmpty(options.Title))
      {
         query = query.Where(t => t.Title.ToLower().Contains(options.Title.ToLower()));
      }
      
      if (options.IsCompleted.HasValue)
      {
         query = query.Where(t => t.IsCompleted == options.IsCompleted.Value);
      }
      
      if (options.CategoryIds != null && options.CategoryIds.Count > 0)
      {
         query = query.Where(t => t.TaskCategories.Any(tc => options.CategoryIds.Contains(tc.CategoryId)));
      }
      
      var totalCount = await query.CountAsync();

      var items = await query
         .OrderByDescending(t => t.CreatedAt)
         .Skip((options.Page - 1) * options.PageSize)
         .Take(options.PageSize)
         .Include(t => t.TaskCategories)
         .ThenInclude(tc => tc.Category)
         .ToListAsync();
      
      return (items, totalCount);
   }
}