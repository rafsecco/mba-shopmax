using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Models.Validations;
using System.Linq.Expressions;

namespace ShopMax.Business.Services;

public class CategoryService : BaseService, ICategoryService
{
	private readonly ICategoryRepository _categoryRepository;

	public CategoryService(ICategoryRepository categoryRepository, INotificator notificador) : base(notificador)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<IEnumerable<Category>> ObterTodos()
	{
		return await _categoryRepository.ObterTodos();
	}

	public async Task<Category> ObterPorId(int id)
	{
		return await _categoryRepository.ObterPorId(id);
	}

	public async Task<IEnumerable<Category>> Buscar(Expression<Func<Category, bool>> predicate)
	{
		return await _categoryRepository.Buscar(predicate);
	}

	public async Task Adicionar(Category Categoria)
	{
		await _categoryRepository.Adicionar(Categoria);

		//if (!ExecutarValidacao(new CategoryValidation(), Categoria)) return;

		//var CategoriaExistente = _categoryRepository.ObterPorId(Categoria.Id);

		//if (CategoriaExistente != null)
		//{
		//	Notificar("Já existe uma categoria com o ID informado!");
		//	return;
		//}

		//await _categoryRepository.Adicionar(Categoria);
	}

	public async Task Atualizar(Category Categoria)
	{
		//if (!ExecutarValidacao(new CategoryValidation(), Categoria)) return;
		await _categoryRepository.Atualizar(Categoria);
	}

	public async Task Deletar(int id)
	{
		await _categoryRepository.Deletar(id);
	}
}
