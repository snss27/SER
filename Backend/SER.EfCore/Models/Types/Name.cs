using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;

namespace SER.EfCore.Models.Types;
public class Name : ValueObject
{
	public String First {  get; }
	public String Second { get; }
	public String? Last { get; }

	private Name(String first, String second, String? last)
	{
		First = first;
		Second = second;
		Last = last;
	}

	public static Result<Name, Error> Create(String first, String second, String? last)
	{
		if (String.IsNullOrWhiteSpace(first)) return new Error("Укажите имя");

		if (String.IsNullOrWhiteSpace(second)) return new Error("Укажите фамилию");

		return new Name(first, second, last);
	}

	protected override IEnumerable<IComparable> GetEqualityComponents()
	{
		yield return First;
		yield return Second;
		yield return Last;
	}
}
