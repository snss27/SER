using System.Text.Json.Serialization;

namespace SER.Tools.Types.Results;

public class OperationResult
{
	public Error[] Errors { get; }

	[JsonIgnore]
	public Boolean IsSuccess => Errors.Length == 0;

	public OperationResult(List<Error> errors)
	{
		Errors = errors.ToArray();
	}

	[JsonConstructor]
	public OperationResult(Error[] errors)
	{
		Errors = errors;
	}

	public static OperationResult Success()
	{
		return new OperationResult(new List<Error>());
	}

	public static OperationResult Fail(Error error)
	{
		return new OperationResult(new List<Error>() { error });
	}

	public static OperationResult Fail(String error)
	{
		return new OperationResult(new List<Error>() { new Error(error) });
	}
}
