namespace ShopMax.Business.Models;

public class Category : Entity
{
	public string? Description { get; set; }

	public IEnumerable<Product>? ProductsList { get; set; }
}
