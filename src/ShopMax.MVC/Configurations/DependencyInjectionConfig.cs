using ShopMax.Business.Interfaces;
using ShopMax.Business.Notifications;
using ShopMax.Business.Services;
using ShopMax.Data;
using ShopMax.Data.Repository;

namespace ShopMax.MVC.Configurations;

public static class DependencyInjectionConfig
{
	public static IServiceCollection AddService(this IServiceCollection services)
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
