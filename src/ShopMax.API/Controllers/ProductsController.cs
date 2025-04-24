using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShopMax.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ShopMaxDbContext _context;

		public ProductsController(ShopMaxDbContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpGet("details/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var produto = await _context.Products.FindAsync(id);

			if (produto == null)
			{
				return NotFound();
			}

			return produto;
		}

		[HttpPut("edit/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
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

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		[HttpDelete("delete/{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductExists(int id)
		{
			return _context.Products.Any(e => e.Id == id);
		}
	}
}
