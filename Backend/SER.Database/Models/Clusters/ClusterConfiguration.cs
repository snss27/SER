using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Clusters;
public class ClusterConfiguration : IEntityTypeConfiguration<ClusterEntity>
{
	public void Configure(EntityTypeBuilder<ClusterEntity> builder)
	{
		builder.ToTable("clusters");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("name");
	}
}
