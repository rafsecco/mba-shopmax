using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopMax.API.Models;
using ShopMax.Business.Models;
using ShopMax.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "Insert the JWT token like this: 'Bearer {your token}'.\n\r\r\n Example: \"Bearer 12345abcdef\"",
		Name = "Authorization",
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddDbContext<ShopMaxDbContext>(options =>
		options.UseSqlite(connectionString));
}
else
{
	builder.Services.AddDbContext<ShopMaxDbContext>(options =>
		options.UseSqlServer(connectionString));
}

builder.Services.AddIdentity<Seller, IdentityRole>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ShopMaxDbContext>();

#region Pegando o Token e gerando a chave encoded
var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(JwtSettingsSection);
var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = jwtSettings.Audience,
		ValidIssuer = jwtSettings.Issuer
	};
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
