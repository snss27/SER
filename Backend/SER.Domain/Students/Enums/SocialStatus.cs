using System.Linq;

namespace SER.Domain.Students.Enums;
public enum SocialStatus
{
	Orphan = 1,
	Invalid = 2,
	OVZ = 3
}

public static class SocialStatusExtensions
{
	public static String DisplayName(this SocialStatus status)
	{
		return status switch
		{
			SocialStatus.Orphan => "Сирота",
			SocialStatus.Invalid => "Инвалидность",
			SocialStatus.OVZ => "ОВЗ",
			_ => throw new Exception()
		};
	}

	public static String DisplayName(this SocialStatus[] statuses)
	{
		if (statuses.Length == 0) return "-";

		String[] statusStrings = [.. statuses.Select(s => s.DisplayName())];
		return String.Join(", ", statusStrings);
	}
}
