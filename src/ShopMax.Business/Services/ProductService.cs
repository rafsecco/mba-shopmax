using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Models.Validations;
using System.Linq.Expressions;

namespace ShopMax.Business.Services;

public class ProductService : BaseService, IProductService
{
	private readonly IProductRepository _productRepository;

	public ProductService(IProductRepository produtoRepository, INotificator notificador) : base(notificador)
	{
		_productRepository = produtoRepository;
	}
	public async Task<IEnumerable<Product>> GetAll()
	{
		return await _productRepository.GetAll();
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

	public async Task Add(Product produto)
	{
		if (!RunValidation(new ProductValidation(), produto)) return;

		var produtoExistente = _productRepository.GetById(produto.Id);

		if (produtoExistente != null)
		{
			Notify("Já existe um produto com o ID informado!");
			return;
		}

		await _productRepository.Add(produto);
	}

	public async Task Update(Product produto)
	{
		if (!RunValidation(new ProductValidation(), produto)) return;
		await _productRepository.Update(produto);
	}

	public async Task Delete(int id)
	{
		await _productRepository.Delete(id);
	}
}
