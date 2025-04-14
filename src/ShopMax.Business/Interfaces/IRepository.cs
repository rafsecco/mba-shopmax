using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
	Task<IEnumerable<TEntity>> ObterTodos();
	Task<TEntity> ObterPorId(int id);
	Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
	Task Adicionar(TEntity entity);
	Task Atualizar(TEntity entity);
	Task Deletar(int id);
	Task<int> SaveChanges();
}
