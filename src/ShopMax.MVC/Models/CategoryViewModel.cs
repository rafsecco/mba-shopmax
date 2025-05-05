using System.ComponentModel.DataAnnotations;

namespace ShopMax.MVC.Models;

public class CategoryViewModel
{
	public int Id { get; set; }

	//[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[StringLength(100, ErrorMessage = "The {0} field must be between {1} and {2} characters long.", MinimumLength = 2)]
	public required string Name { get; set; }


	//[Required(ErrorMessage = "The {0} field needs to be provided.")]
	[StringLength(200, ErrorMessage = "The {0} field must be between {1} and {2} characters long.", MinimumLength = 2)]
	public required string Description { get; set; }

	[Display(Name = "Created In")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public ICollection<ProductViewModel> ProductsList { get; set; } = [];
}
