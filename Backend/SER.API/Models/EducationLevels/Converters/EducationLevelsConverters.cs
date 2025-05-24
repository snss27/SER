using SER.Domain.EducationLevels;

namespace SER.API.Models.EducationLevels.Converters;

public static class EducationLevelsConverters
{
	public static EducationLevelDto ToDto(this EducationLevel educationLevel)
	{
		return new EducationLevelDto(educationLevel.Id, educationLevel.Type, educationLevel.Name, educationLevel.Code, educationLevel.StudyTime);
	}
}
