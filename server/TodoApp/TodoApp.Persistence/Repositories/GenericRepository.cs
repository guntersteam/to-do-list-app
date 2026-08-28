using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Contracts.Exception;
using TodoApp.Domain.Interfaces.Repositories;

namespace TodoApp.Persistence.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
   protected readonly AppDbContext _context;
   protected readonly DbSet<T> _dbSet;

   public GenericRepository(AppDbContext context)
   {
      _context = context;
      _dbSet = _context.Set<T>();
   }

   public async Task<List<T>> GetAll()
   {
      return await _dbSet.ToListAsync();
   }

   public async Task Add(T entity)
   {
      if(entity == null)
         throw new ApiException("Entity is null", 500);

      await _dbSet.AddAsync(entity);
   }

   public async Task<T?> FindById(Guid id)
   {
      return await _dbSet.FindAsync(id);
   }

   public void  Update(T entity)
   {
      if (entity == null)
         throw new ArgumentNullException(nameof(entity));
      _dbSet.Update(entity);
   }

   public async Task DeleteAsync(Guid id)
   {
      var entity = await FindById(id);
      if (entity == null)
      {
         return;
      }
      _dbSet.Remove(entity);
   }

   public async Task<IEnumerable<T>> GetByPredicate(Expression<Func<T, bool>> predicate)
   {
      if (predicate == null)
      {
         throw new ApiException("Predicate is null",500);
      }

      return await _dbSet.Where(predicate).ToListAsync();
   }

   public async Task<int> CountAsync()
   {
      return await _dbSet.CountAsync();
   }

   public void DeleteRange(IEnumerable<T> entities)
   {
      if (entities == null) throw new ApiException("Entities is null", 500);
      _dbSet.RemoveRange(entities);
   }

   public async Task ReloadAsync(T entity)
   {
      await _dbSet.Entry(entity).ReloadAsync();
   }
}