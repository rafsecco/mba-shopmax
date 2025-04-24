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

	public async Task<List<Product>> GetByCategory(int id)
	{
		return await _context.Products
			.Include(c => c.Category)
			.Where(c => c.CategoryId == id)
			.ToListAsync();
	}
}
