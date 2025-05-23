using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Enterprises;
public class EnterpriseConfiguration : IEntityTypeConfiguration<EnterpriseEntity>
{
	public void Configure(EntityTypeBuilder<EnterpriseEntity> builder)
	{
		builder.ToTable("enterprises");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("name");

		builder.Property(x => x.LegalAddress)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("legal_address");

		builder.Property(x => x.ActualAddress)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("actual_address");

		builder.Property(x => x.Address)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("address");

		builder.Property(x => x.INN)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("inn");

		builder.Property(x => x.KPP)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("kpp");

		builder.Property(x => x.ORGN)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("orgn");

		builder.Property(x => x.Phone)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("phone");

		builder.Property(x => x.Mail)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("mail");

		builder.Property(x => x.IsOPK)
			.IsRequired()
			.HasColumnName("is_opk");
	}
}
