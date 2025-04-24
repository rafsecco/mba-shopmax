using ShopMax.Business.Models;

namespace ShopMax.Business.Interfaces;

public interface IProductRepository : IRepository<Product>
{
	Task<List<Product>> GetByCategory(int id);
}
