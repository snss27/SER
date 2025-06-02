using SER.Database.Models.Groups;
using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Services.Clusters.Converters;
using SER.Services.EducationLevels.Converters;
using SER.Services.Employees.Converters;

namespace SER.Services.Groups.Converters;

internal static class GroupExntensions
{
	public static GroupEntity ToEntity(this Group group)
	{
		return new GroupEntity()
		{
			Id = group.Id,
			Number = group.Number,
			StructuralUnit = group.StructuralUnit,
			EducationLevelId = group.EducationLevel.Id,
			EnrollmentYear = group.EnrollmentYear,
			CuratorId = group.Curator?.Id,
			HasCluster = group.HasCluster,
			ClusterId = group.Cluster?.Id,
			CreatedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this GroupEntity entity, Group group)
	{
		entity.Number = group.Number;
		entity.StructuralUnit = group.StructuralUnit;
		entity.EducationLevelId = group.EducationLevel.Id;
		entity.EnrollmentYear = group.EnrollmentYear;
		entity.CuratorId = group.Curator?.Id;
		entity.HasCluster = group.HasCluster;
		entity.ClusterId = group.Cluster?.Id;
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static Group ToDomain(this GroupEntity entity)
	{
		EducationLevel educationLevel = entity.EducationLevel.ToDomain();
		Employee? curator = entity.Curator?.ToDomain();
		Cluster? cluster = entity.Cluster?.ToDomain();

		return Group.Create(entity.Id, entity.Number, entity.StructuralUnit, educationLevel, entity.EnrollmentYear, curator, entity.HasCluster, cluster).Value;
	}
}