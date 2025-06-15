using SER.Domain.EducationLevels.Enums;
using SER.Domain.EducationLevels;
using SER.Domain.Reports.Grouping.Enums;

public class EducationLevelGroupingOptionsDto
{
	public EducationLevelGroupingType Variant { get; set; }

	public EducationLevelDto[]? EducationLevels { get; set; }
	public EducationLevelType[]? EducationLevelTypes { get; set; }
}