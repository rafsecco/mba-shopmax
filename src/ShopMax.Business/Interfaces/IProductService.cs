using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> GetAll();
	Task<IEnumerable<Product>> GetAllWithCategory();
	Task<Product> GetById(int id);
	Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate);
	Task<IEnumerable<Product>> GetProductsFromCategory(int id);
	Task<IEnumerable<Product>> GetProductsFromSeller(int id);
	Task Add(Product entity);
	Task Update(Product entity);
	Task Delete(int id);
}
