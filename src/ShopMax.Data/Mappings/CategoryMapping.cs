using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMax.Business.Models;

namespace ShopMax.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("tb_Categorias");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Nome)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(x => x.Descricao)
			.IsRequired()
			.HasMaxLength(200);
	}
}
