using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
	protected readonly ShopMaxDbContext Db;
	protected readonly DbSet<TEntity> DbSet;

	protected Repository(ShopMaxDbContext db)
	{
		Db = db;
		DbSet = db.Set<TEntity>();
	}

	public virtual async Task<TEntity> ObterPorId(int id)
	{
		return await DbSet.FindAsync(id);
	}

	public virtual async Task<List<TEntity>> ObterTodos()
	{
		return await DbSet.ToListAsync();
	}

	public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
	{
		return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
	}

	public virtual async Task Adicionar(TEntity entity)
	{
		DbSet.Add(entity);
		await SaveChanges();
	}

	public virtual async Task Atualizar(TEntity entity)
	{
		DbSet.Update(entity);
		await SaveChanges();
	}

	public virtual async Task Remover(int id)
	{
		DbSet.Remove(new TEntity { Id = id });
		await SaveChanges();
	}

	public async Task<int> SaveChanges()
	{
		return await Db.SaveChangesAsync();
	}

	public void Dispose()
	{
		Db.Dispose();
	}
}
