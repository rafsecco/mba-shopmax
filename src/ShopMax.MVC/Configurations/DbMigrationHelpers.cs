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
		if (!context.Categorias.Any())
		{
			Category[] categorias =
			{
				new() { Nome = "Bebidas", Descricao = "Bebidas" },
				new() { Nome = "Alimentos", Descricao = "Alimentos" },
				new() { Nome = "Limpeza", Descricao = "Limpeza" },
				new() { Nome = "Higiene", Descricao = "Higiene" },
				new() { Nome = "Outros", Descricao = "Outros" }
			};
			await context.Categorias.AddRangeAsync(categorias);
			await context.SaveChangesAsync();
		}

		if (!context.Users.Any())
		{
			string email = "teste{0}@shopmax.com";

			await context.Users.AddAsync(new()
			{
				Id = 1,
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
				Id = 2,
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

		if (!context.Produtos.Any())
		{
			Product[] produtos =
			{
				new Product() { Nome = "Cerveja", Descricao = "Cerveja", CategoriaId = 1, Preco = 5.00M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 1 },
				new() { Nome = "Refrigerante", Descricao = "Refrigerante", CategoriaId = 1, Preco = 3.50M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 2 },
				new() { Nome = "Arroz", Descricao = "Arroz", CategoriaId = 2, Preco = 2.50M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 1 },
				new() { Nome = "Feijão", Descricao = "Feijão", CategoriaId = 2, Preco = 4.00M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 2 },
				new() { Nome = "Detergente", Descricao = "Detergente", CategoriaId = 3, Preco = 1.50M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 1 },
				new() { Nome = "Sabão em pó", Descricao = "Sabão em pó", CategoriaId = 3, Preco = 6.00M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 2 },
				new() { Nome = "Sabonete", Descricao = "Sabonete", CategoriaId = 4, Preco = 2.00M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 1 },
				new() { Nome = "Shampoo", Descricao = "Shampoo", CategoriaId = 4, Preco = 10.00M, Ativo = true, QuantidadeEstoque = 100, Imagem = "teste.jpg", VendedorId = 2 }
			};
			await context.Produtos.AddRangeAsync(produtos);
			await context.SaveChangesAsync();
		}
	}
}
