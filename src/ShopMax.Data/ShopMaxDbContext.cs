using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;

namespace ShopMax.Data;

public class ShopMaxDbContext : IdentityDbContext<Seller, IdentityRole<int>, int>
{

	public DbSet<Seller> Vendedor { get; set; }
	public DbSet<Product> Produtos { get; set; }
	public DbSet<Category> Categorias{ get; set; }

	public ShopMaxDbContext(DbContextOptions<ShopMaxDbContext> options) : base(options)
	{
		ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		ChangeTracker.AutoDetectChangesEnabled = false;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		foreach (var property in modelBuilder.Model.GetEntityTypes()
			.SelectMany(e => e.GetProperties()
				.Where(p => p.ClrType == typeof(string))))
		{
			property.SetMaxLength(100);
		}

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopMaxDbContext).Assembly);

		// Para impedir que seja feito o deleteCascate
		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
		}

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("DataCadastro").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("DataCadastro").IsModified = false;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("DataCadastro").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("DataCadastro").IsModified = false;
			}
		}

		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}
}
