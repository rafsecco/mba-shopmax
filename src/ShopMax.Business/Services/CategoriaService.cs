using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Models.Validations;

namespace ShopMax.Business.Services;

public class CategoriaService : BaseService, ICategoriaService
{
	private readonly ICategoriaRepository _categoriaRepository;

	public CategoriaService(ICategoriaRepository categoriaRepository, INotificador notificador) : base(notificador)
	{
		_categoriaRepository = categoriaRepository;
	}

	public async Task Adicionar(Categoria Categoria)
	{
		if (!ExecutarValidacao(new CategoriaValidation(), Categoria)) return;

		var CategoriaExistente = _categoriaRepository.ObterPorId(Categoria.Id);

		if (CategoriaExistente != null)
		{
			Notificar("Já existe uma categoria com o ID informado!");
			return;
		}

		await _categoriaRepository.Adicionar(Categoria);
	}

	public async Task Atualizar(Categoria Categoria)
	{
		if (!ExecutarValidacao(new CategoriaValidation(), Categoria)) return;
		await _categoriaRepository.Atualizar(Categoria);
	}

	public async Task Remover(int id)
	{
		await _categoriaRepository.Remover(id);
	}
	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
