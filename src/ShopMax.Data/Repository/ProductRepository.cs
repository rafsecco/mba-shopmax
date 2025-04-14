using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;

namespace ShopMax.Data.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
	private readonly ShopMaxDbContext _context;

	public ProductRepository(ShopMaxDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<List<Product>> ObterPorCategoria(int id)
	{
		return await _context.Produtos
			.Include(c => c.Categoria)
			.Where(c => c.CategoriaId == id)
			.ToListAsync();
	}
}
