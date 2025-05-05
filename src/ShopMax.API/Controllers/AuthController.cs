using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopMax.API.Models;
using ShopMax.Business.Models;
using ShopMax.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShopMax.API.Controllers;

[ApiController]
[Route("api/account")]
public class AuthController : ControllerBase
{
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly ShopMaxDbContext _context;
	private readonly JwtSettings _jwtSettings;

	public AuthController(
		SignInManager<ApplicationUser> signInManager,
		UserManager<ApplicationUser> userManager,
		ShopMaxDbContext context,
		IOptions<JwtSettings> jwtSettings)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_context = context;
		_jwtSettings = jwtSettings.Value;

		if (_jwtSettings == null)
		{
			throw new InvalidOperationException("JwtSettings not injected correctly.");
		}
	}


	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
	{
		if (!ModelState.IsValid) return ValidationProblem(ModelState);

		var user = new ApplicationUser
		{
			UserName = registerUser.Email,
			Email = registerUser.Email,
			EmailConfirmed = true
		};

		var result = await _userManager.CreateAsync(user, registerUser.Password);

		#region Create Seller
		var seller = new Seller
		{
			Name = user.UserName,
			ApplicationUserId = user.Id
		};
		_context.Sellers.Add(seller);
		await _context.SaveChangesAsync();
		#endregion

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
