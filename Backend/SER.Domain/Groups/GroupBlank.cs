using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public class GroupBlank
{
	public ID? Id { get; init; }
	public String? Number { get; init; }
	public StructuralUnit? StructuralUnit { get; init; }
	public EducationLevelDto? EducationLevel { get; init; }
	public Int32? EnrollmentYear { get; init; }
	public EmployeeDto? Curator { get; init; }
	public Boolean? HasCluster { get; init; }
	public ClusterDto? Cluster { get; init; }
}
