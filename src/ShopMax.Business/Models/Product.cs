using System.Text.Json.Serialization;

namespace ShopMax.Business.Models;

public class Product : Entity
{
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public int QuantityStock { get; set; }
	public bool Active { get; set; }
	public string? Image { get; set; }

	// EF Relation
	public int CategoryId { get; set; }
	[JsonIgnore]
	public Category? Category { get; set; }
	public int SellerId { get; set; }
	[JsonIgnore]
	public Seller Seller { get; set; } = null!;
}
