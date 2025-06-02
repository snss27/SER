using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Domain.ValueObjects;
public class Representative : ValueObject
{
	public String? PhoneNumber { get; }
	public String? Alias { get; }

	private Representative(String? phoneNumber, String? alias)
	{
		PhoneNumber = phoneNumber;
		Alias = alias;
	}

	public static Result<Representative, Error> Create(String? phoneNumber, String? alias)
	{
		if(!String.IsNullOrWhiteSpace(phoneNumber) && !Regexs.PhoneRegex.IsMatch(phoneNumber))
		{
			return new Error("Неправильный формат номера телефона представителя");
		}

		return new Representative(phoneNumber, alias);
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return PhoneNumber;
		yield return Alias;
	}
}
