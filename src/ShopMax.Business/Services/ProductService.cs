using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Models.Validations;
using System.Linq.Expressions;

namespace ShopMax.Business.Services;

public class ProductService : BaseService, IProductService
{
	private readonly IProductRepository _productRepository;

	public ProductService(IProductRepository productRepository, INotificator notificador) : base(notificador)
	{
		_productRepository = productRepository;
	}
	public async Task<IEnumerable<Product>> GetAll()
	{
		return await _productRepository.GetAll();
	}

	public async Task<IEnumerable<Product>> GetAllWithCategory()
	{
		return await _productRepository.GetAllWithCategory();
	}

	public async Task<Product> GetById(int id)
	{
		return await _productRepository.GetById(id);
	}

	public async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate)
	{
		return await _productRepository.Find(predicate);
	}

	public async Task<IEnumerable<Product>> GetProductsFromCategory(int id)
	{
		return await _productRepository.GetByCategory(id);
	}

	public async Task<IEnumerable<Product>> GetProductsFromSeller(int id)
	{
		return await _productRepository.GetBySeller(id);
	}

	public async Task Add(Product product)
	{
		if (!RunValidation(new ProductValidation(), product)) return;
		await _productRepository.Add(product);
	}

	public async Task Update(Product product)
	{
		if (!RunValidation(new ProductValidation(), product)) return;
		await _productRepository.Update(product);
	}

	public async Task Delete(int id)
	{
		await _productRepository.Delete(id);
	}
}
