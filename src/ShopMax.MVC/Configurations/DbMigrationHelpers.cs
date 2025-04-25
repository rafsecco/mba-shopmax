using Microsoft.AspNetCore.Identity;
using ShopMax.Business.Models;
using ShopMax.Data;

namespace ShopMax.MVC.Configurations;

public static class DbMigrationHelperExtension
{
	public static void UseDbMigrationHelper(this WebApplication app)
	{
		DbMigrationHelpers.EnsureSeedData(app).Wait();
	}
}

public static class DbMigrationHelpers
{
	public static async Task EnsureSeedData(WebApplication app)
	{
		var services = app.Services.CreateScope().ServiceProvider;
		await EnsureSeedData(services);
	}

	private static async Task EnsureSeedData(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

		var context = scope.ServiceProvider.GetRequiredService<ShopMaxDbContext>();

		if (env.IsDevelopment() || env.IsEnvironment("Docker"))
		{
			await context.Database.EnsureCreatedAsync();
			await EnsureSeedTables(context, serviceProvider);
		}
	}

	private static async Task EnsureSeedTables(ShopMaxDbContext context, IServiceProvider serviceProvider)
	{
		if (!context.Categories.Any())
		{
			Category[] categories =
			{
				new() { Name = "Bebidas", Description = "Bebidas" },
				new() { Name = "Alimentos", Description = "Alimentos" },
				new() { Name = "Limpeza", Description = "Limpeza" },
				new() { Name = "Higiene", Description = "Higiene" },
				new() { Name = "Outros", Description = "Outros" }
			};
			await context.Categories.AddRangeAsync(categories);
			await context.SaveChangesAsync();
		}

		if (!context.Users.Any())
		{
			string email = "teste{0}@shopmax.com";

			await context.Users.AddAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = string.Format(email, 1),
				NormalizedUserName = string.Format(email, 1).ToUpper(),
				Email = string.Format(email, 1),
				NormalizedEmail = string.Format(email, 1).ToUpper(),
				AccessFailedCount = 0,
				LockoutEnabled = false,
				PasswordHash = new PasswordHasher<Seller>().HashPassword(null, "Teste@123"),
				TwoFactorEnabled = false,
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString(),
			});

			await context.Users.AddAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = string.Format(email, 2),
				NormalizedUserName = string.Format(email, 2).ToUpper(),
				Email = string.Format(email, 2),
				NormalizedEmail = string.Format(email, 2).ToUpper(),
				AccessFailedCount = 0,
				LockoutEnabled = false,
				PasswordHash = new PasswordHasher<Seller>().HashPassword(null, "Teste@123"),
				TwoFactorEnabled = false,
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString(),
			});

			await context.SaveChangesAsync();
		}

		if (!context.Products.Any())
		{
			Product[] products =
			{
				new Product() { Name = "Cerveja", Description = "Cerveja", CategoryId = 1, Price = 5.00M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 1 },
				new() { Name = "Refrigerante", Description = "Refrigerante", CategoryId = 1, Price = 3.50M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 2 },
				new() { Name = "Arroz", Description = "Arroz", CategoryId = 2, Price = 2.50M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 1 },
				new() { Name = "Feijão", Description = "Feijão", CategoryId = 2, Price = 4.00M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 2 },
				new() { Name = "Detergente", Description = "Detergente", CategoryId = 3, Price = 1.50M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 1 },
				new() { Name = "Sabão em pó", Description = "Sabão em pó", CategoryId = 3, Price = 6.00M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 2 },
				new() { Name = "Sabonete", Description = "Sabonete", CategoryId = 4, Price = 2.00M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 1 },
				new() { Name = "Shampoo", Description = "Shampoo", CategoryId = 4, Price = 10.00M, Active = true, QuantityStock = 100, Image = "teste.jpg", SellerId = 2 }
			};
			await context.Products.AddRangeAsync(products);
			await context.SaveChangesAsync();
		}
	}
}
