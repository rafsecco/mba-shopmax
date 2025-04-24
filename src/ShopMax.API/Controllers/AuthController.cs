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
[Route("api/account")]
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


	[HttpPost("register")]
	public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
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
			return Ok(GenerateJwt());
		}

		return Problem("Failed to register user");
	}

	[HttpPost("login")]
	public async Task<ActionResult> Login(LoginUserViewModel loginUser)
	{
		if (!ModelState.IsValid) return ValidationProblem(ModelState);

		var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

		if (result.Succeeded)
		{
			return Ok(GenerateJwt());
		}

		return Problem("Incorrect username or password.");
	}

	private string GenerateJwt()
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

		var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
			Issuer = _jwtSettings.Issuer,
			Audience = _jwtSettings.Audience,
			Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expires),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		});

		var encodedToken = tokenHandler.WriteToken(token);

		return encodedToken;
	}
}
