using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;

namespace ShopMax.MVC.Controllers;

[Authorize]
[Route("categorias")]
public class CategoriasController : Controller
{
	private readonly ShopMaxDbContext _context;

	public CategoriasController(ShopMaxDbContext context)
	{
		_context = context;
	}

	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		return View(await _context.Categorias.ToListAsync());
	}

	[Route("detalhes/{id:int}")]
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var categoria = await _context.Categorias
			.FirstOrDefaultAsync(m => m.Id == id);
		if (categoria == null)
		{
			return NotFound();
		}

		return View(categoria);
	}

	[Route("novo")]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost("novo")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Descricao,Id,Nome,DataCadastro")] Categoria categoria)
	{
		if (ModelState.IsValid)
		{
			_context.Add(categoria);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View(categoria);
	}

	[Route("editar/{id:int}")]
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var categoria = await _context.Categorias.FindAsync(id);
		if (categoria == null)
		{
			return NotFound();
		}
		return View(categoria);
	}

	[HttpPost("editar/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Descricao,Id,Nome,DataCadastro")] Categoria categoria)
	{
		if (id != categoria.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(categoria);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoriaExists(categoria.Id))
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
		return View(categoria);
	}

	[Route("excluir/{id:int}")]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var categoria = await _context.Categorias
			.FirstOrDefaultAsync(m => m.Id == id);
		if (categoria == null)
		{
			return NotFound();
		}

		return View(categoria);
	}

	[HttpPost("excluir/{id:int}"), ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var categoria = await _context.Categorias.FindAsync(id);
		if (categoria != null)
		{
			_context.Categorias.Remove(categoria);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool CategoriaExists(int id)
	{
		return _context.Categorias.Any(e => e.Id == id);
	}
}
