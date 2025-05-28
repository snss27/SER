using SER.Domain.Clusters;
using SER.Domain.Employees;
using SER.Domain.Employees.Converters;
using SER.Domain.Clusters.Converters;
using SER.Domain.EducationLevels;
using SER.Domain.EducationLevels.Converters;

namespace SER.Domain.Groups.Converters;

public static class GroupsConverters
{
	public static GroupDto ToDto(this Group group)
	{
		EducationLevelDto educationLevel = group.EducationLevel.ToDto();
		EmployeeDto? employee = group.Curator?.ToDto();
		ClusterDto? cluster = group.Cluster?.ToDto();

		return new GroupDto(
			group.Id,
			group.Number,
			group.StructuralUnit,
			educationLevel,
			group.EnrollmentYear,
			employee,
			group.HasCluster,
			cluster
		);
	}

	public static Group ToDomain(this GroupDto dto)
	{
		EducationLevel educationLevel = dto.EducationLevel.ToDomain();
		Employee? curator = dto.Curator?.ToDomain();
		Cluster? cluster = dto.Cluster?.ToDomain();

		return Group.Create(dto.Id, dto.Number, dto.StructuralUnit, educationLevel, dto.EnrollmentYear, curator, dto.HasCluster, cluster).Value;
	}
}
