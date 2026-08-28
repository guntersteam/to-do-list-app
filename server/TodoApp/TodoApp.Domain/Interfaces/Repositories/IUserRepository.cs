using TodoApp.Domain.Models;

namespace TodoApp.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
   Task<bool> IsUserExist(string email,string? username = null);
}