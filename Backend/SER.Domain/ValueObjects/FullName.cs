using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;

namespace SER.EfCore.Models.Types;
public class FullName : ValueObject
{
	public String First {  get; }
	public String Second { get; }
	public String? Last { get; }

	private FullName(String first, String second, String? last)
	{
		First = first;
		Second = second;
		Last = last;
	}

	public static Result<FullName, Error> Create(String? first, String? second, String? last)
	{
		if (String.IsNullOrWhiteSpace(first)) return new Error("Укажите имя");

		if (String.IsNullOrWhiteSpace(second)) return new Error("Укажите фамилию");

		return new FullName(first, second, last);
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return First;
		yield return Second;
		yield return Last;
	}
}
