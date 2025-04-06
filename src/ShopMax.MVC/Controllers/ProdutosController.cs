using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;

namespace ShopMax.MVC.Controllers;

[Authorize]
[Route("produtos")]
public class ProdutosController : Controller
{
	private readonly ShopMaxDbContext _context;

	public ProdutosController(ShopMaxDbContext context)
	{
		_context = context;
	}

	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		var shopMaxDbContext = _context.Produtos.Include(p => p.Categoria).Include(p => p.Vendedor);
		return View(await shopMaxDbContext.ToListAsync());
	}

	[Route("detalhes/{id:int}")]
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var produto = await _context.Produtos
			.Include(p => p.Categoria)
			.Include(p => p.Vendedor)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (produto == null)
		{
			return NotFound();
		}

		return View(produto);
	}

	[Route("novo")]
	public IActionResult Create()
	{
		ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao");
		ViewData["VendedorId"] = new SelectList(_context.Users, "Id", "Id");
		return View();
	}

	[HttpPost("novo")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Descricao,Preco,QuantidadeEstoque,Ativo,Imagem,CategoriaId,VendedorId,Id,Nome,DataCadastro")] Produto produto)
	{
		if (ModelState.IsValid)
		{
			_context.Add(produto);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
		ViewData["VendedorId"] = new SelectList(_context.Users, "Id", "Id", produto.VendedorId);
		return View(produto);
	}

	[Route("editar/{id:int}")]
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var produto = await _context.Produtos.FindAsync(id);
		if (produto == null)
		{
			return NotFound();
		}
		ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
		ViewData["VendedorId"] = new SelectList(_context.Users, "Id", "Id", produto.VendedorId);
		return View(produto);
	}

	[HttpPost("editar/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Descricao,Preco,QuantidadeEstoque,Ativo,Imagem,CategoriaId,VendedorId,Id,Nome,DataCadastro")] Produto produto)
	{
		if (id != produto.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(produto);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProdutoExists(produto.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));
		}
		ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
		ViewData["VendedorId"] = new SelectList(_context.Users, "Id", "Id", produto.VendedorId);
		return View(produto);
	}

	[Route("excluir/{id:int}")]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var produto = await _context.Produtos
			.Include(p => p.Categoria)
			.Include(p => p.Vendedor)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (produto == null)
		{
			return NotFound();
		}

		return View(produto);
	}

	[HttpPost("excluir/{id:int}"), ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var produto = await _context.Produtos.FindAsync(id);
		if (produto != null)
		{
			_context.Produtos.Remove(produto);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool ProdutoExists(int id)
	{
		return _context.Produtos.Any(e => e.Id == id);
	}
}
