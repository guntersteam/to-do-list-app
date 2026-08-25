using TodoApp.Domain.Contracts.Category;
using TodoApp.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoApp.Domain.Interfaces.Services;

public interface ICategoryService
{
   Task CreateCategory(Guid userId, CreateCategoryRequest request, CancellationToken cancellationToken);
   Task<List<CategoryDto>> GetUserCategories(Guid userId, CancellationToken cancellationToken);
   Task DeleteCategory(Guid userId, Guid categoryId, CancellationToken cancellationToken);
   Task UpdateCategory(Guid userId,UpdateCategoryRequest request, CancellationToken cancellationToken);
}