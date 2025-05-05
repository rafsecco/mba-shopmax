using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Data;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

[Authorize]
[Route("product")]
public class ProductsController : BaseController
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly ShopMaxDbContext _context;
	private readonly IProductService _productService;
	private readonly ICategoryService _categoryService;
	private readonly IMapper _mapper;

	public ProductsController(
		UserManager<ApplicationUser> userManager,
		ShopMaxDbContext context,
		IProductService productService,
		ICategoryService categoryService,
		IMapper mapper,
		INotificator notificator) : base(notificator)
	{
		_userManager = userManager;
		_context = context;
		_productService = productService;
		_categoryService = categoryService;
		_mapper = mapper;
	}

	[Route("list")]
	public async Task<IActionResult> Index()
	{
		var seller = await GetSeller();
		if (seller == null) return NoContent();
		return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetProductsFromSeller(seller.Id)));
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
		var productViewModel = await GetCategories(new ProductViewModel());
		productViewModel.SellerId = (await GetSeller()).Id;
		return View(productViewModel);
	}

	[Route("create")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(ProductViewModel productViewModel)
	{
		ModelState.Remove("Seller");
		ModelState.Remove("Category");
		ModelState.Remove("Categories");

		if (!ModelState.IsValid) return View(productViewModel);

		var imgPrefix = DateTime.Now.ToString("yyyyMMdd_");
		if (await FileUpload(productViewModel.ImageFile, imgPrefix, "ImageFile"))
		{
			productViewModel.Image = imgPrefix + productViewModel.ImageFile.FileName;
		}
		else
		{
			return View(productViewModel);
		}

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

		var productDb = await _productService.GetById(id);

		ModelState.Remove("Seller");
		ModelState.Remove("Category");
		ModelState.Remove("Categories");
		if (!ModelState.IsValid) return View(productViewModel);

		productViewModel.Image = productDb.Image;
		if (productViewModel.ImageFile != null)
		{
			var imgPrefix = DateTime.Now.ToString("yyyyMMdd_");
			if (await FileUpload(productViewModel.ImageFile, imgPrefix, "ImageFile"))
			{
				productViewModel.Image = imgPrefix + productViewModel.ImageFile.FileName;
			}
		}

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
		var category = _mapper.Map<CategoryViewModel>(await _categoryService.GetById(productViewModel.CategoryId));
		productViewModel.Category = category;
		productViewModel = await GetCategories(productViewModel);
		productViewModel.SellerId = (await GetSeller()).Id;
		return productViewModel;
	}

	private async Task<ProductViewModel> GetCategories(ProductViewModel product)
	{
		product.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetAll());
		return product;
	}

	private async Task<bool> FileUpload(IFormFile file, string imagePrefix, string formItem)
	{
		if (file == null || file.Length <= 0) { return false; }
		var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images", imagePrefix + file.FileName);
		if (System.IO.File.Exists(path))
		{
			ModelState.AddModelError(formItem, "A file with this name already exists!");
			return false;
		}

		using (var stream = new FileStream(path, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}
		return true;
	}

	private async Task<Seller> GetSeller()
	{
		var userId = _userManager.GetUserId(User);
		var seller = await _context.Sellers.FirstOrDefaultAsync(v => v.ApplicationUserId == userId);
		return seller;
	}
}
