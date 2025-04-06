namespace ShopMax.Business.Models;

public abstract class Entity
{
	public int Id { get; set; }
	public string Nome { get; set; }
	public DateTime DataCadastro { get; set; } = DateTime.Now;
}
