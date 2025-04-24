using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;

namespace ShopMax.Data.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
	private readonly ShopMaxDbContext _context;

	public CategoryRepository(ShopMaxDbContext context) : base(context)
	{
		_context = context;
	}
}
