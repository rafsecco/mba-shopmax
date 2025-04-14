using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

//[Authorize]
[Route("categorias")]
public class CategoriesController : BaseController
{
	private readonly ICategoryService _categoryService;
	private readonly IMapper _mapper;

	public CategoriesController(
		ICategoryService categoryService,
		IMapper mapper,
		INotificator notificator) : base(notificator)
	{
		_categoryService = categoryService;
		_mapper = mapper;
	}

	[AllowAnonymous]
	[Route("lista-de-categorias")]
	public async Task<IActionResult> Index()
	{
		return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.ObterTodos()));
	}

	[Route("dados-da-categoria/{id:int}")]
	public async Task<IActionResult> Details(int id)
	{
		var categoryViewModel = await ObterCategoriaModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("nova-categoria")]
	public IActionResult Create()
	{
		return View();
	}

	[Route("nova-categoria")]
	[HttpPost]
	public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
	{
		if (!ModelState.IsValid) return View(categoryViewModel);

		var category = _mapper.Map<Category>(categoryViewModel);
		await _categoryService.Adicionar(category);

		if (!OperacaoValida()) return View(categoryViewModel);

		TempData["Sucesso"] = "Categoria criada com sucesso!";

		return RedirectToAction("Index");
	}

	[Route("editar-categoria/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var categoryViewModel = await ObterCategoriaModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("editar-categoria/{id:int}")]
	[HttpPost]
	public async Task<IActionResult> Edit(int id, CategoryViewModel categoryViewModel)
	{
		if (id != categoryViewModel.Id) return NotFound();

		if (!ModelState.IsValid) return View(categoryViewModel);

		var category = _mapper.Map<Category>(categoryViewModel);
		await _categoryService.Atualizar(category);

		if (!OperacaoValida()) return View(await ObterCategoriaModel(id));

		TempData["Sucesso"] = "Categoria editada com sucesso!";

		return RedirectToAction("Index");
	}

	[Route("excluir-categoria/{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var categoryViewModel = await ObterCategoriaModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("excluir-categoria/{id:int}")]
	[HttpPost, ActionName("Delete")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var categoryViewModel = await ObterCategoriaModel(id);

		if (categoryViewModel == null) return NotFound();

		await _categoryService.Deletar(id);

		if (!OperacaoValida()) return View(categoryViewModel);

		TempData["Sucesso"] = "Categoria excluida com sucesso!";

		return RedirectToAction("Index");
	}

	private async Task<CategoryViewModel> ObterCategoriaModel(int id)
	{
		return _mapper.Map<CategoryViewModel>(await _categoryService.ObterPorId(id));
	}

}
