using SER.Database.Models.AdditionalQualifications;
using SER.Domain.AdditionalQualifications;

namespace SER.Services.AdditionalQualifications.Converters;
internal static class AdditionalQualificationExtensions
{
	public static AdditionalQualificationEntity ToEntity(this AdditionalQualification additionalQualification)
	{
		return new AdditionalQualificationEntity()
		{
			Id = additionalQualification.Id,
			Name = additionalQualification.Name,
			Code = additionalQualification.Code,
			StudyTime = additionalQualification.StudyTime,
			CreatedDateTimeUtc =  DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this AdditionalQualificationEntity entity, AdditionalQualification additionalQualification)
	{
		entity.Name = additionalQualification.Name;
		entity.Code = additionalQualification.Code;
		entity.StudyTime = additionalQualification.StudyTime;
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static AdditionalQualification ToDomain(this AdditionalQualificationEntity entity)
	{
		return AdditionalQualification.Create(entity.Id, entity.Name, entity.Code, entity.StudyTime).Value;
	}
}
