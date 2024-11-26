using SER.Domain.AdditionalQualifications;
using SER.Services.AdditionalQualifications.Models;

namespace SER.Services.AdditionalQualifications.Converters;
internal static class AdditionalQualificationsConverter
{
	public static AdditionalQualification ToAdditionalQualification(this AdditionalQualificationDB db)
	{
		return new AdditionalQualification(db.Id, db.Name, db.Code, db.StudyTime, db.CreatedDateTimeUtc, db.ModifiedDateTimeUtc);
	}

	public static AdditionalQualification[] ToAdditionalQualifications(this AdditionalQualificationDB[] dbs)
	{
		return dbs.Select(ToAdditionalQualification).ToArray();
	}
}
