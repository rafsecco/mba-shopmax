using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ShopMax.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly ShopMaxDbContext _context;

		public ProdutosController(ShopMaxDbContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProdutos()
		{
			return await _context.Produtos.ToListAsync();
		}

		[HttpGet("detalhes/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Product>> GetProduto(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);

			if (produto == null)
			{
				return NotFound();
			}

			return produto;
		}

		[HttpPut("editar/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> PutProduto(int id, Product produto)
		{
			if (id != produto.Id)
			{
				return BadRequest();
			}

			_context.Entry(produto).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProdutoExists(id))
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

		[HttpPost("novo")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Product>> PostProduto(Product produto)
		{
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
		}

		[HttpDelete("excluir/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> DeleteProduto(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null)
			{
				return NotFound();
			}

			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProdutoExists(int id)
		{
			return _context.Produtos.Any(e => e.Id == id);
		}
	}
}
