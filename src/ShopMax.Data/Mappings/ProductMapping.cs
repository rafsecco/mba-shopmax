using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMax.Business.Models;

namespace ShopMax.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("tb_Products");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(x => x.Price)
			.IsRequired()
			.HasPrecision(18, 2);

		builder.Property(x => x.QuantityStock)
			.IsRequired();

		builder.Property(x => x.Image)
			.HasMaxLength(200);

		// Relationship 1:N between Seller and Product
		builder.HasOne(x => x.Seller)
			.WithMany(x => x.ProdutctsList)
			.HasForeignKey(x => x.SellerId);

		// Relationship 1:N between Category and Product
		builder.HasOne(x => x.Category)
			.WithMany(x => x.ProductsList)
			.HasForeignKey(x => x.CategoryId);
	}
}
