using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using ShopMax.API.Models;
using ShopMax.Business.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShopMax.API.Controllers;

[ApiController]
[Route("api/conta")]
public class AuthController : ControllerBase
{
	private readonly SignInManager<Seller> _signInManager;
	private readonly UserManager<Seller> _userManager;
	private readonly JwtSettings _jwtSettings;

	public AuthController(
		SignInManager<Seller> signInManager,
		UserManager<Seller> userManager,
		IOptions<JwtSettings> jwtSettings)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_jwtSettings = jwtSettings.Value;
	}


	[HttpPost("registrar")]
	public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
	{
		if (!ModelState.IsValid) return ValidationProblem(ModelState);

		var user = new Seller
		{
			UserName = registerUser.Email,
			Email = registerUser.Email,
			EmailConfirmed = true
		};

		var result = await _userManager.CreateAsync(user, registerUser.Password);

		if (result.Succeeded)
		{
			await _signInManager.SignInAsync(user, false);
			return Ok(GerarJwt());
		}

		return Problem("Falha ao registrar o usuário");
	}

	[HttpPost("login")]
	public async Task<ActionResult> Login(LoginUserViewModel loginUser)
	{
		if (!ModelState.IsValid) return ValidationProblem(ModelState);

		var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

		if (result.Succeeded)
		{
			return Ok(GerarJwt());
		}

		return Problem("Usuário ou senha incorretos.");
	}

	private string GerarJwt()
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

		var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
			Issuer = _jwtSettings.Emissor,
			Audience = _jwtSettings.Audiencia,
			Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		});

		var encodedToken = tokenHandler.WriteToken(token);

		return encodedToken;
	}
}
