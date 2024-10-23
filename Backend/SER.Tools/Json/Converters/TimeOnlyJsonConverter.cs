using System.Text.Json;
using System.Text.Json.Serialization;

namespace SER.Tools.Json.Converters;
public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
	private readonly string serializationFormat = "HH:mm:ss";

	public override TimeOnly Read(ref Utf8JsonReader reader,
							Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetString();
		return TimeOnly.Parse(value!);
	}

	public override void Write(Utf8JsonWriter writer, TimeOnly value,
										JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString(serializationFormat));
}
