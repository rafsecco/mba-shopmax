using FluentValidation;

namespace ShopMax.Business.Models.Validations;

public class ProductValidation : AbstractValidator<Product>
{
	public ProductValidation()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("The {PropertyName} field needs to be provided.")
			.Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters long.");

		RuleFor(c => c.Description)
			.NotEmpty().WithMessage("The {PropertyName} field needs to be provided.")
			.Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters long.");

		RuleFor(c => c.Price)
			.NotEmpty().WithMessage("The {PropertyName} field needs to be provided.")
			.GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}.");
	}
}
