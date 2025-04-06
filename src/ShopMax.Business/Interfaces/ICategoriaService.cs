using ShopMax.Business.Models;

namespace ShopMax.Business.Interfaces;

public interface ICategoriaService : IDisposable
{
	Task Adicionar(Categoria categoria);
	Task Atualizar(Categoria categoria);
	Task Remover(int id);
}
