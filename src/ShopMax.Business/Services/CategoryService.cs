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

	public async Task<IEnumerable<Category>> GetAll()
	{
		return await _categoryRepository.GetAll();
	}

	public async Task<Category> GetById(int id)
	{
		return await _categoryRepository.GetById(id);
	}

	public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate)
	{
		return await _categoryRepository.Find(predicate);
	}

	public async Task Add(Category Categoria)
	{
		await _categoryRepository.Add(Categoria);

		//if (!ExecutarValidacao(new CategoryValidation(), Categoria)) return;

		//var CategoriaExistente = _categoryRepository.ObterPorId(Categoria.Id);

		//if (CategoriaExistente != null)
		//{
		//	Notificar("Já existe uma categoria com o ID informado!");
		//	return;
		//}

		//await _categoryRepository.Adicionar(Categoria);
	}

	public async Task Update(Category Categoria)
	{
		//if (!ExecutarValidacao(new CategoryValidation(), Categoria)) return;
		await _categoryRepository.Update(Categoria);
	}

	public async Task Delete(int id)
	{
		await _categoryRepository.Delete(id);
	}
}
