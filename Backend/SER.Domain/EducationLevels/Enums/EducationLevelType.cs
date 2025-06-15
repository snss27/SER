namespace SER.Domain.EducationLevels.Enums;
public enum EducationLevelType
{
	Speciality = 1,
	Profession = 2,
	ProfessionalEducation = 3
}

public static class EducationLevelTypeExtensions
{
	public static String DisplayName(this EducationLevelType type)
	{
		return type switch
		{
			EducationLevelType.Speciality => "Специальность",
			EducationLevelType.Profession => "Профессия",
			EducationLevelType.ProfessionalEducation => "Профессиональное обучение",
			_ => throw new Exception()
		};
	}
}
