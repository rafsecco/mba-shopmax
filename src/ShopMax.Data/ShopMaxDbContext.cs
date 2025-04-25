using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMax.Business.Models;

namespace ShopMax.Data;

public class ShopMaxDbContext : IdentityDbContext<ApplicationUser>
{
	public DbSet<Seller> Sellers { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }

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

		// To prevent deleteCascate from being performed
		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
		}

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("CreatedAt").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("CreatedAt").IsModified = false;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("CreatedAt").CurrentValue = DateTime.Now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Property("CreatedAt").IsModified = false;
			}
		}

		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}
}
