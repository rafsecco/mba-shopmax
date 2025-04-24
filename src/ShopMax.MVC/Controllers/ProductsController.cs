using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

//[Authorize]
[Route("product")]
public class ProductsController : BaseController
{
	private readonly IProductService _productService;
	private readonly ICategoryService _categoryService;
	private readonly IMapper _mapper;

	public ProductsController(
		IProductService productService,
		ICategoryService categoryService,
		IMapper mapper,
		INotificator notificator) : base(notificator)
	{
		_productService = productService;
		_categoryService = categoryService;
		_mapper = mapper;
	}

	[AllowAnonymous]
	[Route("list")]
	public async Task<IActionResult> Index()
	{
		return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetAll()));
	}

	[Route("details/{id:int}")]
	public async Task<IActionResult> Details(int id)
	{
		var productViewModel = await GetProductModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("create")]
	public async Task<IActionResult> Create()
	{
		var produtoViewModel = await GetCategories(new ProductViewModel());

		return View(produtoViewModel);
	}

	[Route("create")]
	[HttpPost]
	public async Task<IActionResult> Create(ProductViewModel productViewModel)
	{
		if (!ModelState.IsValid) return View(productViewModel);

		var product = _mapper.Map<Product>(productViewModel);
		await _productService.Add(product);

		if (!ValidateOperation()) return View(productViewModel);

		TempData["Success"] = "Product created successfully!";

		return RedirectToAction("Index");
	}

	[Route("edit/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var productViewModel = await GetProductModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("edit/{id:int}")]
	[HttpPost]
	public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
	{
		if (id != productViewModel.Id) return NotFound();

		if (!ModelState.IsValid) return View(productViewModel);

		var product = _mapper.Map<Product>(productViewModel);
		await _productService.Update(product);

		if (!ValidateOperation()) return View(await GetProductModel(id));

		TempData["Success"] = "Product edited successfully!";

		return RedirectToAction("Index");
	}

	[Route("delete/{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var productViewModel = await GetProductModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("delete/{id:int}")]
	[HttpPost, ActionName("Delete")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var productViewModel = await GetProductModel(id);

		if (productViewModel == null) return NotFound();

		await _productService.Delete(id);

		if (!ValidateOperation()) return View(productViewModel);

		TempData["Success"] = "Product deleted successfully!";

		return RedirectToAction("Index");
	}

	private async Task<ProductViewModel> GetProductModel(int id)
	{
		var productViewModel = _mapper.Map<ProductViewModel>(await _productService.GetById(id));
		productViewModel = await GetCategories(productViewModel);
		return productViewModel;
	}

	private async Task<ProductViewModel> GetCategories(ProductViewModel produto)
	{
		produto.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetAll());
		return produto;
	}

}
