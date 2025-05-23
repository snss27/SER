using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Groups;

public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
{
	public void Configure(EntityTypeBuilder<GroupEntity> builder)
	{
		builder.ToTable("groups");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Number)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("number");

		builder.Property(x => x.StructuralUnit)
			.IsRequired()
			.HasColumnName("sctructural_unit");

		builder.Property(x => x.EducationLevelId)
			.HasColumnName("education_level_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.EducationLevel)
			.WithMany()
			.HasForeignKey(x => x.EducationLevelId)
			.HasConstraintName("fk_group_education_level")
			.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.EnrollmentYear)
			.IsRequired()
			.HasColumnName("enrollment_year");

		builder.Property(x => x.CuratorId)
			.HasColumnName("curator_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.Curator)
			.WithMany()
			.HasForeignKey(x => x.CuratorId)
			.HasConstraintName("fk_group_curator")
			.OnDelete(DeleteBehavior.SetNull);

		builder.Property(x => x.HasCluster)
			.IsRequired()
			.HasColumnName("has_cluster");

		builder.Property(x => x.ClusterId)
			.HasColumnName("cluster_id")
			.HasColumnType("bytea");

		builder.HasOne(x => x.Cluster)
			.WithMany()
			.HasForeignKey(x => x.ClusterId)
			.HasConstraintName("fk_group_cluster")
			.OnDelete(DeleteBehavior.SetNull);
	}
}
