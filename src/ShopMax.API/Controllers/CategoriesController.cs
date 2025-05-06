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
	public class CategoriesController : ControllerBase
	{
		private readonly ShopMaxDbContext _context;

		public CategoriesController(ShopMaxDbContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _context.Categories.ToListAsync();
		}

		[AllowAnonymous]
		[HttpGet("details/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var categoria = await _context.Categories.FindAsync(id);

			if (categoria == null)
			{
				return NotFound();
			}

			return categoria;
		}

		[HttpPut("edit/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok();
		}

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCategory", routeValues: new { id = category.Id }, category);
		}

		[HttpDelete("delete/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return Ok();
		}

		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.Id == id);
		}
	}
}
