using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMax.Business.Models;

namespace ShopMax.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("tb_Produtos");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Nome)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(x => x.Descricao)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(x => x.Preco)
			.IsRequired()
			.HasPrecision(10, 2);

		builder.Property(x => x.QuantidadeEstoque)
			.IsRequired();

		builder.Property(x => x.Imagem)
			.HasMaxLength(200);
	}
}
