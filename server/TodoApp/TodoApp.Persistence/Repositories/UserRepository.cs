using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
   public UserRepository(AppDbContext context) : base(context)
   {
   }
}