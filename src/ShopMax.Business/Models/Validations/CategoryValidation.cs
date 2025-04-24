using FluentValidation;

namespace ShopMax.Business.Models.Validations;

public class CategoryValidation : AbstractValidator<Category>
{
	public CategoryValidation()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("The {PropertyName} field needs to be provided.")
			.Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters long.");

		RuleFor(c => c.Description)
			.NotEmpty().WithMessage("The {PropertyName} field needs to be provided.")
			.Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters long.");
	}
}
