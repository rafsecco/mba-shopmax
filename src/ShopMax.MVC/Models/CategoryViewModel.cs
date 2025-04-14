using System.ComponentModel.DataAnnotations;

namespace ShopMax.MVC.Models;

public class CategoryViewModel
{
	public int Id { get; set; }

	[Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
	[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
	public string Nome { get; set; }


	[Display(Name = "Descrição")]
	[Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
	[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
	public string Descricao { get; set; }

	[Display(Name = "Data de cadastro")]
	public DateTime DataCadastro { get; set; } = DateTime.Now;

	public ICollection<ProductViewModel> Produtos { get; set; } = [];
}
