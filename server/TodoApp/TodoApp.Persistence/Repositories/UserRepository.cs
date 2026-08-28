using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Models;

namespace TodoApp.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
   public UserRepository(AppDbContext context) : base(context)
   {
   }

   public async Task<bool> IsUserExist(string email, string username)
   {
      return await _context.Users
         .AnyAsync(u => u.Email == email || u.Username == username);
   }
}