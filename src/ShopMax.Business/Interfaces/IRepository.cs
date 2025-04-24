using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
	Task<IEnumerable<TEntity>> GetAll();
	Task<TEntity> GetById(int id);
	Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
	Task Add(TEntity entity);
	Task Update(TEntity entity);
	Task Delete(int id);
	Task<int> SaveChanges();
}
