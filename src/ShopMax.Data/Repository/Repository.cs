using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
	protected readonly ShopMaxDbContext _context;
	protected readonly DbSet<TEntity> _dbSet;

	protected Repository(ShopMaxDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<TEntity>();
	}

	public async Task<IEnumerable<TEntity>> ObterTodos()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<TEntity> ObterPorId(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
	{
		return await _dbSet.Where(predicate).ToListAsync();
	}

	public async Task Adicionar(TEntity entity)
	{
		_dbSet.Add(entity);
		await SaveChanges();
	}

	public async Task Atualizar(TEntity entity)
	{
		_dbSet.Update(entity);
		await SaveChanges();
	}

	public async Task Deletar(int id)
	{
		var entity = await ObterPorId(id);
		if (entity != null)
		{
			_dbSet.Remove(entity);
			await SaveChanges();
		}
	}

	public async Task<int> SaveChanges()
	{
		return await _context.SaveChangesAsync();
	}

	public void Dispose() => _context.Dispose();//throw new NotImplementedException();
}
