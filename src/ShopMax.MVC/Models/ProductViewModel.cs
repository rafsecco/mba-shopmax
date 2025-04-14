using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShopMax.MVC.Models;

public class ProductViewModel
{
	public int Id { get; set; }

	[Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
	[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
	public string Nome { get; set; } = null!;

	[DisplayName("Descrição")]
	[Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
	[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
	public string Descricao { get; set; }

	
	[Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
	[Range(0, double.MaxValue, ErrorMessage = "O preço não pode ser negativo.")]
	[Column(TypeName = "decimal(18,2)")]
	[DisplayName("Preço")]
	public decimal Preco { get; set; }

	[DisplayName("Quantidade em Estoque")]
	[Required(ErrorMessage = "O campo {0} precisa ser fornecido.")]
	[Range(0, int.MaxValue, ErrorMessage = "O valor precisa ser positivo")]
	public int QuantidadeEstoque { get; set; }

	[DisplayName("Ativo?")]
	public bool Ativo { get; set; }

	public string? Imagem { get; set; }

	[Display(Name = "Data de cadastro")]
	public DateTime DataCadastro { get; set; } = DateTime.Now;

	[NotMapped]
	public IFormFile ImageFile { get; set; }


	[Required(ErrorMessage = "O campo {0} é obrigatório")]
	[DisplayName("Categoria")]
	public int CategoriaId { get; set; }

	public CategoryViewModel Categoria { get; set; }

	public IEnumerable<CategoryViewModel> Categorias { get; set; }



	
	//public Category Categoria { get; set; } = null!;

	//// FK for Seller
	//public int VendedorId { get; set; }
	//public Seller Vendedor { get; set; } = null!;
}
