using Microsoft.AspNetCore.Identity;

namespace ShopMax.Business.Models;

public class Seller : IdentityUser<int>
{
	public IEnumerable<Product>? ProdutctsList { get; set; }
}
