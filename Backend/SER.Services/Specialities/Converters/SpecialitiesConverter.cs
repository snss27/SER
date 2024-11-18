using SER.Domain.Specialities;
using SER.Services.Specialities.Models;

namespace SER.Services.Specialities.Converters;
internal static class SpecialitiesConverter
{
	public static Speciality ToSpeciality(this SpecialityDB db)
	{
		return new Speciality(
			db.Id,
			db.Name,
			db.Code,
			db.StudyYears,
			db.StudyMonths,
			db.CreatedDateTimeUtc,
			db.ModifiedDateTimeUtc
		);
	}

	public static Speciality[] ToSpecialities(this SpecialityDB[] dbs)
	{
		return dbs.Select(ToSpeciality).ToArray();
	}
}
