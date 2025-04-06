using FluentValidation;

namespace ShopMax.Business.Models.Validations;

public class ProdutoValidation : AbstractValidator<Produto>
{
	public ProdutoValidation()
	{
		RuleFor(c => c.Nome)
			.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
			.Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

		RuleFor(c => c.Descricao)
			.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
			.Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

		RuleFor(c => c.Preco)
			.GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
	}
}
