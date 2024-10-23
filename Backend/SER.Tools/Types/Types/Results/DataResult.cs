using System.Text.Json.Serialization;

namespace SER.Tools.Types.Results;

public readonly struct DataResult<T>
{
	public T? Data { get; }
	public Error[] Errors { get; } 
	public Boolean IsSuccess => !Errors?.Any() ?? true;

	[JsonConstructor]
	public DataResult(T data, Error[]? errors = null)
	{
		Data = data;
		Errors = errors ?? new Error[0];
	}

	public static DataResult<T> Success(T data)
	{
		return new(data);
	}

	public static DataResult<T?> Failed(Error[] errors)
	{
		return new(default, errors);
	}

	public static DataResult<T?> Failed(String error)
	{
		return new(default, new[] { new Error(error) });
	}
}
