using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMax.Business.Models;

namespace ShopMax.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("tb_Categories");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(200);
	}
}
