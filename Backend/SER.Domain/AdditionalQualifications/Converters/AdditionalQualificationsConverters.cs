namespace SER.Domain.AdditionalQualifications.Converters;

public static class AdditionalQualificationsConverters
{
	public static AdditionalQualificationDto ToDto(this AdditionalQualification additionalQualification)
	{
		return new AdditionalQualificationDto(additionalQualification.Id, additionalQualification.Name, additionalQualification.Code, additionalQualification.StudyTime);
	}

	public static AdditionalQualification ToDomain(this AdditionalQualificationDto dto)
	{
		return AdditionalQualification.Create(dto.Id, dto.Name, dto.Code, dto.StudyTime).Value;
	}
}
