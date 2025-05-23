using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.WorkPlaces;
public class WorkPlaceConfiguration : IEntityTypeConfiguration<WorkPlaceEntity>
{
	public void Configure(EntityTypeBuilder<WorkPlaceEntity> builder)
	{
		builder.ToTable("work_places");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.EnterpriseId)
			.HasColumnName("enterprise_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.Enterprise)
			.WithMany()
			.HasForeignKey(x => x.EnterpriseId)
			.HasConstraintName("fk_workplace_enterprise")
			.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.Post)
			.HasMaxLength(200)
			.HasColumnName("post");

		builder.Property(x => x.WorkBookExtractFiles)
			.IsRequired()
			.HasColumnName("work_book_extract_files")
			.HasColumnType("varchar[]");

		builder.Property(x => x.StartDate)
			.HasColumnType("date")
			.HasColumnName("start_date");

		builder.Property(x => x.FinishDate)
			.HasColumnType("date")
			.HasColumnName("finish_date");

		builder.Property(x => x.StudentId)
			.IsRequired()
			.HasColumnName("student_id")
			.HasColumnType("bytea");

		builder.Property(x => x.IsCurrent)
			.IsRequired()
			.HasColumnName("is_current");
	}
}
