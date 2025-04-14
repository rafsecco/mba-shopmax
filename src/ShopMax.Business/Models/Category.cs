namespace ShopMax.Business.Models;

public class Category : Entity
{
	public string Descricao { get; set; }

	public IEnumerable<Product>? Produtos { get; set; }
}
