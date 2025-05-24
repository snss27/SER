using SER.Database.Models.EducationLevels;
using SER.Domain.EducationLevels;

namespace SER.Services.EducationLevels.Converters;
internal static class EducationLevelExtensions
{
	public static EducationLevelEntity ToEntity(this EducationLevel educationLevel)
	{
		return new EducationLevelEntity()
		{
			Id = educationLevel.Id,
			Type = educationLevel.Type,
			Code = educationLevel.Code,
			Name = educationLevel.Name,
			StudyTime = educationLevel.StudyTime,
			CreatedDateTimeUtc = DateTime.UtcNow,
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this EducationLevelEntity entity, EducationLevel educationLevel)
	{
		entity.Type = educationLevel.Type;
		entity.Code = educationLevel.Code;
		entity.Name = educationLevel.Name;
		entity.StudyTime = educationLevel.StudyTime;
		entity.ModifiedDateTimeUtc = DateTime.UtcNow;
	}

	public static EducationLevel ToDomain(this EducationLevelEntity entity)
	{
		return EducationLevel.Create(entity.Id, entity.Type, entity.Name, entity.Code, entity.StudyTime).Value;
	}
}
