using System.Text.Json.Serialization;

namespace SER.Tools.Types.Results;

public class Error
{
	public String? Key { get; }
	public String Message { get; }

	[JsonConstructor]
	public Error(String message, String? key = null)
	{
		Key = key;
		Message = message;
	}

	public override String ToString()
	{
		return String.IsNullOrEmpty(Key) ? Message : $"({Key}) {Message}";
	}
}