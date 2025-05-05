using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ShopMax.Business.Models;

namespace ShopMax.MVC.Models;

public class ProductViewModel
{
	public int Id { get; set; }

	[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 2)]
	public string? Name { get; set; }

	[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[StringLength(200, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 2)]
	public string? Description { get; set; }


	[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[Range(0, double.MaxValue, ErrorMessage = "The {0} cannot be negative.")]
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }


	[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[Range(0, int.MaxValue, ErrorMessage = "The {0} cannot be negative.")]
	[DisplayName("Quantity in Stock")]
	public int QuantityStock { get; set; }

	[DisplayName("Active?")]
	public bool Active { get; set; }

	[NotMapped]
	[DisplayName("Product image")]
	public IFormFile? ImageFile { get; set; }
	public string? Image { get; set; }

	[Display(Name = "Created In")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public IEnumerable<CategoryViewModel> Categories { get; set; }

	// FK for Seller
	public int SellerId { get; set; }
	public Seller Seller { get; set; } = null!;

	// FK for Category
	public int CategoryId { get; set; }
	public CategoryViewModel Category { get; set; } = null!;
}
