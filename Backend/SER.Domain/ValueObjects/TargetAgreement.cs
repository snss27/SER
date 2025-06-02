using CSharpFunctionalExtensions;
using SER.Domain.Enterprises;
using SER.Tools.Types.Results;

namespace SER.Domain.ValueObjects;
public class TargetAgreement : ValueObject
{
	public Boolean Exist { get; }
	public String? Number { get; }
	public Enterprise? Enterprise { get; }
	public DateTime? Date { get; }
	public String[] Files { get; } = default!;

	private TargetAgreement(Boolean exist, String? number, Enterprise? enterprise, DateTime? date, String[] files)
	{
		Exist = exist;
		Number = number;
		Enterprise = enterprise;
		Date = date;
		Files = files;
	}

	public static Result<TargetAgreement, Error> Create(Boolean? exist, String? number, Enterprise? enterprise, DateTime? date, String[] files)
	{
		if (exist is null) return new Error("Укажите, есть ли договор на целевое обучение");

		if (exist.Value)
		{
			return new TargetAgreement(true, number, enterprise, date, files);
		}
		else
		{
			return new TargetAgreement(false, null, null, null, []);
		}
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return Exist;
		yield return Number;
		yield return Enterprise?.Id;
		yield return Date;
		yield return Files;
	}
}
