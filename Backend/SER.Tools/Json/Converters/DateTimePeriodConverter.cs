using System.Text.Json;
using System.Text.Json.Serialization;
using SER.Tools.Types;

namespace SER.Tools.Json.Converters;

public class DateTimePeriodConverter : JsonConverter<DateTimePeriod>
{
	public override DateTimePeriod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException("Expected StartArray token");

		reader.Read();
		var from = reader.TokenType == JsonTokenType.Null ? (DateTime?)null : reader.GetDateTime();

		reader.Read();
		var to = reader.TokenType == JsonTokenType.Null ? (DateTime?)null : reader.GetDateTime();

		reader.Read();
		if (reader.TokenType != JsonTokenType.EndArray)
			throw new JsonException("Expected EndArray token");

		return new DateTimePeriod { From = from, To = to };
	}

	public override void Write(Utf8JsonWriter writer, DateTimePeriod value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		if (value.From.HasValue)
			writer.WriteStringValue(value.From.Value);
		else
			writer.WriteNullValue();

		if (value.To.HasValue)
			writer.WriteStringValue(value.To.Value);
		else
			writer.WriteNullValue();

		writer.WriteEndArray();
	}
}
