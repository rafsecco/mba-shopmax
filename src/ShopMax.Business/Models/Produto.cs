namespace ShopMax.Business.Models;

public class Produto : Entity
{
	public string Descricao { get; set; }
	public decimal Preco { get; set; }
	public int QuantidadeEstoque { get; set; }
	public bool Ativo { get; set; }
	public string Imagem { get; set; }


	public Categoria Categoria { get; set; }
	public int CategoriaId { get; set; }
	public Vendedor Vendedor { get; set; }
	public string VendedorId { get; set; }
}
