using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;

namespace SER.Domain.ValueObjects;
public class Army : ValueObject
{
	public Boolean MustServe { get; }
	public DateTime? CallDate { get; }
	public String[] SubpoenaFiles { get; } = default!;

	private Army(Boolean mustServe, DateTime? callDate, String[] subpoenaFiles)
	{
		MustServe = mustServe;
		CallDate = callDate;
		SubpoenaFiles = subpoenaFiles;
	}

	public static Result<Army, Error> Create(Boolean? mustServe, DateTime? callDate, String[] subpoenaFiles)
	{
		if (mustServe is null) return new Error("Укажите, подлежит ли студент призыву");

		if (mustServe.Value)
		{
			return new Army(true, callDate, subpoenaFiles);
		}
		else
		{
			return new Army(false, null, []);
		}
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return MustServe;
		yield return CallDate;
		yield return SubpoenaFiles;
	}
}
