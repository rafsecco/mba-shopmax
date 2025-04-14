using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> ObterTodos();
	Task<Product> ObterPorId(int id);
	Task<IEnumerable<Product>> Buscar(Expression<Func<Product, bool>> predicate);
	Task<IEnumerable<Product>> ObterProdutosDaCategoria(int id);
	Task Adicionar(Product entity);
	Task Atualizar(Product entity);
	Task Deletar(int id);
}
