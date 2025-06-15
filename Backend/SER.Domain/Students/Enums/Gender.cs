namespace SER.Domain.Students.Enums;
public enum Gender
{
	Male = 1,
	Female = 2
}

public static class GenderExtensions
{
	public static String DisplayName(this Gender gender)
	{
		return gender switch
		{
			Gender.Male => "М",
			Gender.Female => "Ж",
			_ => throw new Exception()
		};
	}
}
