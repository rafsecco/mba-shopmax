using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

//[Authorize]
[Route("produtos")]
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
	[Route("lista-de-produtos")]
	public async Task<IActionResult> Index()
	{
		return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productService.ObterTodos()));
	}

	[Route("dados-do-produto/{id:int}")]
	public async Task<IActionResult> Details(int id)
	{
		var productViewModel = await ObterProdutoModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("novo-produto")]
	public async Task<IActionResult> Create()
	{
		var produtoViewModel = await PopularCategorias(new ProductViewModel());

		return View(produtoViewModel);
	}

	[Route("novo-produto")]
	[HttpPost]
	public async Task<IActionResult> Create(ProductViewModel productViewModel)
	{
		if (!ModelState.IsValid) return View(productViewModel);

		var product = _mapper.Map<Product>(productViewModel);
		await _productService.Adicionar(product);

		if (!OperacaoValida()) return View(productViewModel);

		TempData["Sucesso"] = "Produto criado com sucesso!";

		return RedirectToAction("Index");
	}

	[Route("editar-protudo/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var productViewModel = await ObterProdutoModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("editar-protudo/{id:int}")]
	[HttpPost]
	public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
	{
		if (id != productViewModel.Id) return NotFound();

		if (!ModelState.IsValid) return View(productViewModel);

		var product = _mapper.Map<Product>(productViewModel);
		await _productService.Atualizar(product);

		if (!OperacaoValida()) return View(await ObterProdutoModel(id));

		TempData["Sucesso"] = "Produto editado com sucesso!";

		return RedirectToAction("Index");
	}

	[Route("excluir-produto/{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var productViewModel = await ObterProdutoModel(id);

		if (productViewModel == null)
		{
			return NotFound();
		}

		return View(productViewModel);
	}

	[Route("excluir-produto/{id:int}")]
	[HttpPost, ActionName("Delete")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var productViewModel = await ObterProdutoModel(id);

		if (productViewModel == null) return NotFound();

		await _productService.Deletar(id);

		if (!OperacaoValida()) return View(productViewModel);

		TempData["Sucesso"] = "Produto excluido com sucesso!";

		return RedirectToAction("Index");
	}

	private async Task<ProductViewModel> ObterProdutoModel(int id)
	{
		var productViewModel = _mapper.Map<ProductViewModel>(await _productService.ObterPorId(id));
		productViewModel = await PopularCategorias(productViewModel);
		return productViewModel;
	}

	private async Task<ProductViewModel> PopularCategorias(ProductViewModel produto)
	{
		produto.Categorias = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.ObterTodos());
		return produto;
	}

}
