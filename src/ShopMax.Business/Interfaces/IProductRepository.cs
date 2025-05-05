using ShopMax.Business.Models;

namespace ShopMax.Business.Interfaces;

public interface IProductRepository : IRepository<Product>
{
	Task<List<Product>> GetAllWithCategory();
	Task<List<Product>> GetByCategory(int id);
	Task<List<Product>> GetBySeller(int id);
}
