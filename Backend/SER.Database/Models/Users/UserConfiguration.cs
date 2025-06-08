using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SER.Database.Models.Employees;
using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Users;
public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder.ToTable("users");

		builder.ConfigureBaseEntity();

		builder.Property(x => x.Login)
			.IsRequired()
			.HasMaxLength(200)
			.HasColumnName("login");

		builder.Property(x => x.Password)
			.IsRequired()
			.HasConversion(new EncryptedStringConverter())
			.HasMaxLength(200)
			.HasColumnName("password");
	}
}
