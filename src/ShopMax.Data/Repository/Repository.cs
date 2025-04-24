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

	public async Task<IEnumerable<TEntity>> GetAll()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<TEntity> GetById(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
	{
		return await _dbSet.Where(predicate).ToListAsync();
	}

	public async Task Add(TEntity entity)
	{
		_dbSet.Add(entity);
		await SaveChanges();
	}

	public async Task Update(TEntity entity)
	{
		_dbSet.Update(entity);
		await SaveChanges();
	}

	public async Task Delete(int id)
	{
		var entity = await GetById(id);
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
