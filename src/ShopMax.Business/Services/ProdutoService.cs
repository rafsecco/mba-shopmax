using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Models.Validations;

namespace ShopMax.Business.Services;

public class ProdutoService : BaseService, IProdutoService
{
	private readonly IProdutoRepository _produtoRepository;

	public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
	{
		_produtoRepository = produtoRepository;
	}

	public async Task Adicionar(Produto produto)
	{
		if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

		var produtoExistente = _produtoRepository.ObterPorId(produto.Id);

		if (produtoExistente != null)
		{
			Notificar("Já existe um produto com o ID informado!");
			return;
		}

		await _produtoRepository.Adicionar(produto);
	}

	public async Task Atualizar(Produto produto)
	{
		if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
		await _produtoRepository.Atualizar(produto);
	}

	public async Task Remover(int id)
	{
		await _produtoRepository.Remover(id);
	}
	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
