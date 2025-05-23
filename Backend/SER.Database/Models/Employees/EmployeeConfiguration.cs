using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Employees;
public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
	public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
	{
		builder.ToTable("employees");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("name");

		builder.Property(x => x.SecondName)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("second_name");

		builder.Property(x => x.LastName)
			.IsRequired(false)
			.HasMaxLength(200)
			.HasColumnName("last_name");
	}
}
