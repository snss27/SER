using SER.Domain.Clusters;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Domain.Groups.Enums;
using SER.Domain.Reports.Grouping.Enums;
using SER.Tools.Types;

public class GroupGroupingOptionsDto
{
	public GroupGroupingType Type { get; set; }

	public GroupDto[]? Groups { get; set; }
	public StructuralUnit[]? StructuralUnits { get; set; }
	public EducationLevelGroupingOptionsDto? EducationLevelGroupingOptions { get; set; }

	public DateTimePeriod? EnrollmentYearPeriod { get; set; }

	public EmployeeDto[]? Curators { get; set; }
	public ClusterDto[]? Clusters { get; set; }
}