using SER.Domain.AdditionalQualifications;

namespace SER.API.Models.AdditionalQualifications.Converters;

public static class AdditionalQualificationsConverters
{
	public static AdditionalQualificationDto ToDto(this AdditionalQualification additionalQualification)
	{
		return new AdditionalQualificationDto(additionalQualification.Id, additionalQualification.Name, additionalQualification.Code, additionalQualification.StudyTime);
	}
}
