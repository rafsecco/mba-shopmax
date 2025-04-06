using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;
using ShopMax.Data;
using ShopMax.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDefaultIdentity<Vendedor>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<ShopMaxDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseDbMigrationHelper();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
