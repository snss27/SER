using SER.Domain.EducationLevels;
using SER.Domain.EducationLevels.Enums;
using SER.Domain.Reports.Grouping.Enums;

namespace SER.Domain.Reports.Grouping.Extensions;
public static class EducationLevelGroupingOptionsMatcher
{
	public static void Match(
		this EducationLevelGroupingOptionsDto options,
		Action<EducationLevelDto[]> onEducationLevels,
		Action<EducationLevelType[]> onEducationLevelTypes)
	{
		switch (options.Variant)
		{
			case EducationLevelGroupingType.EducationLevels when options.EducationLevels is not null:
				onEducationLevels(options.EducationLevels);
				break;

			case EducationLevelGroupingType.EducationLevelTypes when options.EducationLevelTypes is not null:
				onEducationLevelTypes(options.EducationLevelTypes);
				break;

			default:
				throw new InvalidOperationException("Некорректная конфигурация уровней образования");
		}
	}
}
