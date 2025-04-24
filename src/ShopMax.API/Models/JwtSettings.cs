namespace ShopMax.API.Models;

public class JwtSettings
{
	public string? Secret { get; set; }
	public int Expires { get; set; }
	public string? Issuer { get; set; }
	public string? Audience { get; set; }
}
