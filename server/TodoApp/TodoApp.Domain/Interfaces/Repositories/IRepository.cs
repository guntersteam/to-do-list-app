using System.Linq.Expressions;

namespace TodoApp.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity: class
{
   Task<List<TEntity>> GetAll();
   Task Add(TEntity entity);
   Task<TEntity?> FindById(Guid id);
   void Update(TEntity item);
   Task DeleteAsync(Guid id);
   Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
   Task<int> CountAsync();
   void DeleteRange(IEnumerable<TEntity> entities);
   Task ReloadAsync(TEntity entity);
}