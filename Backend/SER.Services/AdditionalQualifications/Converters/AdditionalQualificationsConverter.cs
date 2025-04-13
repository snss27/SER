using SER.Domain.AdditionalQualifications;
using SER.Services.AdditionalQualifications.Models;

namespace SER.Services.AdditionalQualifications.Converters;
internal static class AdditionalQualificationsConverter
{
	public static AdditionalQualification ToAdditionalQualification(this AdditionalQualificationDB db)
	{
		return new AdditionalQualification()
		{
			Id = db.Id,
			Code = db.Code,
			Name = db.Name,
			StudyTime = db.StudyTime,
		};
	}

	public static AdditionalQualification[] ToAdditionalQualifications(this AdditionalQualificationDB[] dbs)
	{
		return dbs.Select(ToAdditionalQualification).ToArray();
	}
}
