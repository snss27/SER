namespace SER.Domain.Students.Enums;
public enum StudentStatus
{
	Active = 1,
	Expelled = 2,
	Finished = 3,
	AcademicLeave = 4
}

public static class StudentStatusExtensions
{
	public static String DisplayName(this StudentStatus status)
	{
		return status switch
		{
			StudentStatus.Active => "Обучается",
			StudentStatus.Expelled => "Отчислен",
			StudentStatus.Finished => "Отчислен, в связи с окончанием обучения",
			StudentStatus.AcademicLeave => "Академический отпуск",
			_ => throw new Exception()
		};
	}
}
