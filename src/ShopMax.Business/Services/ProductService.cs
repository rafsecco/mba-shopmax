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
	public async Task<IEnumerable<Product>> ObterTodos()
	{
		return await _productRepository.ObterTodos();
	}

	public async Task<Product> ObterPorId(int id)
	{
		return await _productRepository.ObterPorId(id);
	}

	public async Task<IEnumerable<Product>> Buscar(Expression<Func<Product, bool>> predicate)
	{
		return await _productRepository.Buscar(predicate);
	}

	public async Task<IEnumerable<Product>> ObterProdutosDaCategoria(int id)
	{
		return await _productRepository.ObterPorCategoria(id);
	}

	public async Task Adicionar(Product produto)
	{
		if (!ExecutarValidacao(new ProductValidation(), produto)) return;

		var produtoExistente = _productRepository.ObterPorId(produto.Id);

		if (produtoExistente != null)
		{
			Notificar("Já existe um produto com o ID informado!");
			return;
		}

		await _productRepository.Adicionar(produto);
	}

	public async Task Atualizar(Product produto)
	{
		if (!ExecutarValidacao(new ProductValidation(), produto)) return;
		await _productRepository.Atualizar(produto);
	}

	public async Task Deletar(int id)
	{
		await _productRepository.Deletar(id);
	}
}
