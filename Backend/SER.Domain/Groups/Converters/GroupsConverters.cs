namespace SER.Domain.Groups.Converters;

public static class GroupsConverters
{
	public static GroupDto ToDto(this Group group)
	{
		return new GroupDto(
			group.Id,
			group.Number,
			group.StructuralUnit,
			group.EducationLevel,
			group.EnrollmentYear,
			group.Curator,
			group.HasCluster,
			group.Cluster
		);
	}
}
