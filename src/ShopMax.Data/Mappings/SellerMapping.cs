using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMax.Business.Models;

namespace ShopMax.Data.Mappings;

public class SellerMapping : IEntityTypeConfiguration<Seller>
{
	public void Configure(EntityTypeBuilder<Seller> builder)
	{
		builder.ToTable("tb_Sellers");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(x => x.ApplicationUserId)
			.IsRequired()
			.HasMaxLength(36);
	}
}
