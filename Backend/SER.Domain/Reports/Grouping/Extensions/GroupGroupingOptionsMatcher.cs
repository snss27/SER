using SER.Domain.Clusters;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Domain.Groups.Enums;
using SER.Domain.Reports.Grouping.Enums;
using SER.Tools.Types;

namespace SER.Domain.Reports.Grouping.Extensions;
public static class GroupGroupingOptionsMatcher
{
	public static void Match(this GroupGroupingOptionsDto options, Action<GroupDto[]> onGroups,
		Action<StructuralUnit[]> onStructuralUnits,
		Action<EducationLevelGroupingOptionsDto> onEducationLevel,
		Action<DateTimePeriod> onEnrollmentYearPeriod,
		Action<EmployeeDto[]> onCurators,
		Action<ClusterDto[]> onClusters,
		Action onNotGrouping)
	{
		switch (options.Type)
		{
			case GroupGroupingType.Groups when options.Groups is not null:
				onGroups(options.Groups);
				break;

			case GroupGroupingType.StructuralUnits when options.StructuralUnits is not null:
				onStructuralUnits(options.StructuralUnits);
				break;

			case GroupGroupingType.EducationLevel when options.EducationLevelGroupingOptions is not null:
				onEducationLevel(options.EducationLevelGroupingOptions);
				break;

			case GroupGroupingType.EnrollmentYearPeriod when options.EnrollmentYearPeriod is not null:
				onEnrollmentYearPeriod(options.EnrollmentYearPeriod);
				break;

			case GroupGroupingType.Curators when options.Curators is not null:
				onCurators(options.Curators);
				break;

			case GroupGroupingType.Clusters when options.Clusters is not null:
				onClusters(options.Clusters);
				break;

			case GroupGroupingType.NotGrouping:
				onNotGrouping();
				break;

			default:
				throw new InvalidOperationException("Некорректные параметры группировки");
		}
	}
}
