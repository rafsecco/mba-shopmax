using ShopMax.Business.Models;

namespace ShopMax.Business.Interfaces;

public interface IProdutoService : IDisposable
{
	Task Adicionar(Produto produto);
	Task Atualizar(Produto produto);
	Task Remover(int id);
}
