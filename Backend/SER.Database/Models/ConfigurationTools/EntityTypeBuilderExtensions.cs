using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SER.Database.Models.ConfigurationTools;
public static class EntityTypeBuilderExtensions
{
	public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("id")
			.HasColumnType("bytea");

		builder.Property(x => x.CreatedDateTimeUtc)
			.HasColumnName("created_datetime_utc")
			.HasColumnType("timestamp")
			.IsRequired();

		builder.Property(x => x.ModifiedDateTimeUtc)
			.HasColumnType("timestamp")
			.HasColumnName("modified_datetime_utc");
	}

	public static PropertyBuilder<List<TEnum>> HasEnumListConversion<TEnum>(this PropertyBuilder<List<TEnum>> builder)
		where TEnum : Enum
	{
		var propBuilder = builder.HasConversion(new EnumListToIntArrayConverter<TEnum>());

		propBuilder.Metadata.SetValueComparer(new EnumListValueComparer<TEnum>());

		return propBuilder;
	}
}
