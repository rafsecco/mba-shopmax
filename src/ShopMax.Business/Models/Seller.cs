namespace ShopMax.Business.Models;

public class Seller : Entity
{
	public string? IdentityId { get; set; }

	public IEnumerable<Product>? ProdutctsList { get; set; }
}
