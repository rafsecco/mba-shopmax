namespace ShopMax.Business.Models;

public class Product : Entity
{
	public string Descricao { get; set; }
	public decimal Preco { get; set; }
	public int QuantidadeEstoque { get; set; }
	public bool Ativo { get; set; }
	public string Imagem { get; set; }

	// EF Relation
	public Category Categoria { get; set; }
	public int CategoriaId { get; set; }
	public Seller Vendedor { get; set; }
	public int VendedorId { get; set; }
}
