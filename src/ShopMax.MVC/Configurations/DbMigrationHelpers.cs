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

		await context.Database.EnsureCreatedAsync();

		if (env.IsDevelopment() || env.IsEnvironment("Docker"))
		{
			await EnsureSeedTables(context, serviceProvider);
		}
	}

	private static async Task EnsureSeedTables(ShopMaxDbContext context, IServiceProvider serviceProvider)
	{
		if (!context.Categories.Any())
		{
			Category[] categories =
			{
				new() { Name = "Drinks", Description = "Drinks" },
				new() { Name = "Food", Description = "Food" },
				new() { Name = "Cleaning", Description = "Cleaning" },
				new() { Name = "Hygiene", Description = "Hygiene" },
				new() { Name = "Others", Description = "Others" }
			};
			await context.Categories.AddRangeAsync(categories);
			await context.SaveChangesAsync();
		}

		#region Users and Seller
		var userId_1 = Guid.NewGuid();
		var userId_2 = Guid.NewGuid();
		string email = "test{0}@shopmax.com";

		if (!context.Users.Any())
		{
			ApplicationUser[] users =
			{
				new ApplicationUser()
				{
					Id = userId_1.ToString(),
					UserName = string.Format(email, 1),
					NormalizedUserName = string.Format(email, 1).ToUpper(),
					Email = string.Format(email, 1),
					NormalizedEmail = string.Format(email, 1).ToUpper(),
					AccessFailedCount = 0,
					LockoutEnabled = false,
					PasswordHash = new PasswordHasher<Seller>().HashPassword(null, "Test@123"),
					TwoFactorEnabled = false,
					ConcurrencyStamp = Guid.NewGuid().ToString(),
					EmailConfirmed = true,
					SecurityStamp = Guid.NewGuid().ToString()
				},
				new ApplicationUser()
				{
					Id = userId_2.ToString(),
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
				}
			};

			await context.Users.AddRangeAsync(users);
			await context.SaveChangesAsync();
		}

		if (!context.Sellers.Any())
		{
			Seller[] sellers =
			{
				new Seller() { Name = "Test 1", ApplicationUserId = userId_1.ToString() },
				new Seller() { Name = "Test 2", ApplicationUserId = userId_2.ToString() },
			};

			await context.Sellers.AddRangeAsync(sellers);
			await context.SaveChangesAsync();
		}
		#endregion

		if (!context.Products.Any())
		{
			Product[] products =
			{
				new Product() { Name = "Beer", Description = "Beer", CategoryId = 1, Price = 5.00M, Active = true, QuantityStock = 100, Image = "Beer_307.png", SellerId = 1 },
				new() { Name = "Soda", Description = "Soda", CategoryId = 1, Price = 3.50M, Active = true, QuantityStock = 100, Image = "Soda_307.png", SellerId = 2 },
				new() { Name = "Rice", Description = "Rice", CategoryId = 2, Price = 2.50M, Active = true, QuantityStock = 100, Image = "Rise_307.png", SellerId = 1 },
				new() { Name = "Beans", Description = "Beans", CategoryId = 2, Price = 4.00M, Active = true, QuantityStock = 100, Image = "Beans_307.png", SellerId = 2 },
				new() { Name = "Detergent", Description = "Detergent", CategoryId = 3, Price = 1.50M, Active = true, QuantityStock = 100, Image = "Detergent_307.png", SellerId = 1 },
				new() { Name = "Washing powder", Description = "Washing powder", CategoryId = 3, Price = 6.00M, Active = true, QuantityStock = 100, Image = "Washing_307.png", SellerId = 2 },
				new() { Name = "Soap", Description = "Soap", CategoryId = 4, Price = 2.00M, Active = true, QuantityStock = 100, Image = "Soap_307.png", SellerId = 1 },
				new() { Name = "Shampoo", Description = "Shampoo", CategoryId = 4, Price = 10.00M, Active = true, QuantityStock = 100, Image = "Shampoo_307.png", SellerId = 2 }
			};
			await context.Products.AddRangeAsync(products);
			await context.SaveChangesAsync();
		}
	}
}
