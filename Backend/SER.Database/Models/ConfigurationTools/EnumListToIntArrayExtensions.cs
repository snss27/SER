using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SER.Database.Models.ConfigurationTools;
public class EnumListToIntArrayConverter<TEnum> : ValueConverter<List<TEnum>, int[]>
	where TEnum : Enum
{
	public EnumListToIntArrayConverter() : base(
		v => v.Select(e => Convert.ToInt32(e)).ToArray(),
		v => v.Select(i => (TEnum)Enum.ToObject(typeof(TEnum), i)).ToList()
	)
	{ }
}

public class EnumListValueComparer<TEnum> : ValueComparer<List<TEnum>>
	where TEnum : Enum
{
	public EnumListValueComparer() : base(
		(l1, l2) => l1.SequenceEqual(l2),
		l => l.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
		l => l.ToList()
	)
	{ }
}
