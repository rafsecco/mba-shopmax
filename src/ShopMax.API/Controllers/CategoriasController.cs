using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;

namespace ShopMax.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriasController : ControllerBase
	{
		private readonly ShopMaxDbContext _context;

		public CategoriasController(ShopMaxDbContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
		{
			return await _context.Categorias.ToListAsync();
		}

		[HttpGet("detalhes/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Categoria>> GetCategoria(int id)
		{
			var categoria = await _context.Categorias.FindAsync(id);

			if (categoria == null)
			{
				return NotFound();
			}

			return categoria;
		}

		[HttpPut("editar/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
		{
			if (id != categoria.Id)
			{
				return BadRequest();
			}

			_context.Entry(categoria).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoriaExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpPost("nova")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
		{
			_context.Categorias.Add(categoria);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
		}

		[HttpDelete("excluir/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> DeleteCategoria(int id)
		{
			var categoria = await _context.Categorias.FindAsync(id);
			if (categoria == null)
			{
				return NotFound();
			}

			_context.Categorias.Remove(categoria);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CategoriaExists(int id)
		{
			return _context.Categorias.Any(e => e.Id == id);
		}
	}
}
