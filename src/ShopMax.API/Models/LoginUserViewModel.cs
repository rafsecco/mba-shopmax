using System.ComponentModel.DataAnnotations;

namespace ShopMax.API.Models;

public class LoginUserViewModel
{
	//[Required(ErrorMessage = "The {PropertyName} field needs to be provided.")]
	[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
	public required string Email { get; set; }

	//[Required(ErrorMessage = "The {PropertyName} field needs to be provided.")]
	[StringLength(100, ErrorMessage = "The {PropertyName} field must be between {MinLength} and {MaxLength} characters long.", MinimumLength = 6)]
	public required string Password { get; set; }
}
