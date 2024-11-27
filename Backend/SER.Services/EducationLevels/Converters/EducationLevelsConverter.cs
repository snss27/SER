using SER.Domain.EducationLevels;
using SER.Services.EducationLevels.Models;

namespace SER.Services.EducationLevels.Converters;
internal static class EducationLevelsConverter
{
	public static EducationLevel ToEducationLevel(this EducationLevelDB db)
	{
		return new EducationLevel(
			db.Id,
			db.Type,
			db.Name,
			db.Code,
			db.StudyTime,
			db.CreatedDateTimeUtc,
			db.ModifiedDateTimeUtc
		);
	}

	public static EducationLevel[] ToEducationLevels(this EducationLevelDB[] dbs)
	{
		return dbs.Select(ToEducationLevel).ToArray();
	}
}
