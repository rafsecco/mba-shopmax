using FluentValidation;

namespace ShopMax.Business.Models.Validations;

public class CategoryValidation : AbstractValidator<Category>
{
	public CategoryValidation()
	{
		RuleFor(c => c.Nome)
			.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
			.Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

		RuleFor(c => c.Descricao)
			.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
			.Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
	}
}
