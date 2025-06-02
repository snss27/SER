using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Domain.ValueObjects;
public class Passport : ValueObject
{
	public String? Number { get; }
	public String? Series { get; }
	public String? IssuedBy { get; }
	public DateTime? IssuedDate { get; }
	public String[] Files { get; } = default!;

	private Passport(String? number, String? series, String? issuedBy, DateTime? issuedDate, String[] files)
	{
		Number = number;
		Series = series;
		IssuedBy = issuedBy;
		IssuedDate = issuedDate;
		Files = files;
	}

	public static Result<Passport, Error> Create(String? number, String? series, String? issuedBy, DateTime? issuedDate, String[] files)
	{
		if (!String.IsNullOrWhiteSpace(number) && !Regexs.PassportNumberRegex.IsMatch(number))
		{
			return new Error("Неправильный формат номера паспорта");
		}

		if (!String.IsNullOrWhiteSpace(series) && !Regexs.PassportSeriesRegex.IsMatch(series))
		{
			return new Error("Неправильный формат серии паспорта");
		}

		return new Passport(number, series, issuedBy, issuedDate, files);
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return Number;
		yield return Series;
		yield return IssuedBy;
		yield return IssuedDate;
		yield return Files;
	}
}
