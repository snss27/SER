using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public record GroupDto(
	ID Id,
	String Number,
	StructuralUnit StructuralUnit,
	EducationLevelDto EducationLevel,
	Int32 EnrollmentYear,
	EmployeeDto? Curator,
	Boolean HasCluster,
	ClusterDto? Cluster
);
