namespace SER.Domain.Groups.Enums;
public enum StructuralUnit
{
	SP1 = 1,
	SP2 = 2,
	SP3 = 3,
	SP4 = 4
}

public static class StructuralUnitExtensions
{
	public static String DisplayName(this StructuralUnit unit)
	{
		return unit switch
		{
			StructuralUnit.SP1 => "СП1",
			StructuralUnit.SP2 => "СП2",
			StructuralUnit.SP3 => "СП3",
			StructuralUnit.SP4 => "СП4",
			_ => throw new Exception()
		};
	}
}
