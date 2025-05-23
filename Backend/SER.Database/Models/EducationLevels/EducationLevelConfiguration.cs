using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.EducationLevels;
public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevelEntity>
{
	public void Configure(EntityTypeBuilder<EducationLevelEntity> builder)
	{
		builder.ToTable("education_levels");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Type)
			.IsRequired()
			.HasColumnName("type");

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
