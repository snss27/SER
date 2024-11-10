using SER.Domain.Groups;
using SER.Services.Groups.Models;

namespace SER.Services.Groups.Converters;
internal static class GroupsConverter
{
	public static Group ToGroup(this GroupDB db)
	{
		return new Group(
			db.Id,
			db.Number,
			db.StructuralUnit,
			db.SpecialityId,
			db.EnrollmentYear,
			db.CuratorName,
			db.CreatedDateTimeUtc,
			db.ModifiedDateTimeUtc
		);
	}

	public static Group[] ToGroups(this GroupDB[] dbs)
	{
		return dbs.Select(ToGroup).ToArray();
	}
}
