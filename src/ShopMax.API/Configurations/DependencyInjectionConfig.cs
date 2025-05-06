using ShopMax.Business.Interfaces;
using ShopMax.Business.Notifications;
using ShopMax.Business.Services;
using ShopMax.Data.Repository;
using ShopMax.Data;

namespace ShopMax.API.Configurations;

public static class DependencyInjectionConfig
{
	public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
	{
		// Data
		services.AddScoped<ShopMaxDbContext>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();

		// Business
		services.AddScoped<INotificator, Notificator>();
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();

		return services;
	}
}
