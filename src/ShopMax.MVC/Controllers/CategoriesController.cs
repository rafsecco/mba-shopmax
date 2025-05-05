using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

[Authorize]
[Route("category")]
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

	[Route("list")]
	public async Task<IActionResult> Index()
	{
		return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetAll()));
	}

	[Route("details/{id:int}")]
	public async Task<IActionResult> Details(int id)
	{
		var categoryViewModel = await GetCategoryModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("create")]
	public IActionResult Create()
	{
		return View();
	}

	[Route("create")]
	[HttpPost]
	public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
	{
		if (!ModelState.IsValid) return View(categoryViewModel);

		var category = _mapper.Map<Category>(categoryViewModel);
		await _categoryService.Add(category);

		if (!ValidateOperation()) return View(categoryViewModel);

		TempData["Success"] = "Category created successfully!";

		return RedirectToAction("Index");
	}

	[Route("edit/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var categoryViewModel = await GetCategoryModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("edit/{id:int}")]
	[HttpPost]
	public async Task<IActionResult> Edit(int id, CategoryViewModel categoryViewModel)
	{
		if (id != categoryViewModel.Id) return NotFound();

		if (!ModelState.IsValid) return View(categoryViewModel);

		var category = _mapper.Map<Category>(categoryViewModel);
		await _categoryService.Update(category);

		if (!ValidateOperation()) return View(await GetCategoryModel(id));

		TempData["Success"] = "Category edited successfully!";

		return RedirectToAction("Index");
	}

	[Route("delete/{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var categoryViewModel = await GetCategoryModel(id);

		if (categoryViewModel == null)
		{
			return NotFound();
		}

		return View(categoryViewModel);
	}

	[Route("delete/{id:int}")]
	[HttpPost, ActionName("Delete")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var categoryViewModel = await GetCategoryModel(id);

		if (categoryViewModel == null) return NotFound();

		await _categoryService.Delete(id);

		if (!ValidateOperation()) return View(categoryViewModel);

		TempData["Success"] = "Category deleted successfully!";

		return RedirectToAction("Index");
	}

	private async Task<CategoryViewModel> GetCategoryModel(int id)
	{
		return _mapper.Map<CategoryViewModel>(await _categoryService.GetById(id));
	}

}
