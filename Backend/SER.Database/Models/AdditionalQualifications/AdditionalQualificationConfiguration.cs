using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.AdditionalQualifications;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Clusters;
public class AdditionalQualificationConfiguration : IEntityTypeConfiguration<AdditionalQualificationEntity>
{
	public void Configure(EntityTypeBuilder<AdditionalQualificationEntity> builder)
	{
		builder.ToTable("additional_qualifications");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("name");

		builder.Property(x => x.Code)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("code");

		builder.Property(x => x.StudyTime)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("study_time");
	}
}
