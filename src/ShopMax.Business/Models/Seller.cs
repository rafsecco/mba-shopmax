namespace ShopMax.Business.Models;

public class Seller : Entity
{
	public string? ApplicationUserId { get; set; }

	public IEnumerable<Product>? ProdutctsList { get; set; }
}
