namespace SER.Domain.EducationLevels.Converters;

public static class EducationLevelsConverters
{
	public static EducationLevelDto ToDto(this EducationLevel educationLevel)
	{
		return new EducationLevelDto(educationLevel.Id, educationLevel.Type, educationLevel.Name, educationLevel.Code, educationLevel.StudyTime);
	}

	public static EducationLevel ToDomain(this EducationLevelDto dto)
	{
		return EducationLevel.Create(dto.Id, dto.Type, dto.Name, dto.Code, dto.StudyTime).Value;
	}
}
