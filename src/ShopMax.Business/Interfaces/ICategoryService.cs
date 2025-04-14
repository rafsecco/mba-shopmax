using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<Category>> ObterTodos();
	Task<Category> ObterPorId(int id);
	Task<IEnumerable<Category>> Buscar(Expression<Func<Category, bool>> predicate);
	Task Adicionar(Category entity);
	Task Atualizar(Category entity);
	Task Deletar(int id);
}
