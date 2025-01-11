using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;

namespace SER.Domain.Groups.Converters;

public static class GroupDtoConverter
{
	public static GroupDto ToGroupDto(this Group group, EducationLevel? educationLevel, Employee? curator, Cluster? cluster)
	{
		return new GroupDto(
			group.Id,
			group.Number,
			group.StructuralUnit,
			educationLevel,
			group.EnrollmentYear,
			curator,
			group.HasCluster,
			cluster
		);
	}

	public static GroupDto[] ToGroupDtos(this Group[] groups, EducationLevel[] educationLevels, Employee[] curators, Cluster[] clusters)
	{
		List<GroupDto> result = [];
		result.AddRange(
			from @group in groups
			let educationLevel = educationLevels.FirstOrDefault(el => el.Id == @group.EducationLevelId)
			let curator = curators.FirstOrDefault(curator => curator.Id == @group.CuratorId)
			let cluster = clusters.FirstOrDefault(cluster => cluster.Id == @group.ClusterId)
			select new GroupDto(
				@group.Id,
				@group.Number,
				@group.StructuralUnit,
				educationLevel,
				@group.EnrollmentYear,
				curator,
				@group.HasCluster,
				cluster
			)
		);

		return result.ToArray();
	}
}