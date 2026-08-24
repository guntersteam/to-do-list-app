using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Repositories;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
   public CategoryRepository(AppDbContext context) : base(context)
   {
   }
}