using SER.Tools.Types.Times;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SER.Tools.Json.Converters;

public class TimeJsonConverter : JsonConverter<Time>
{
	public override Time Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		Int32 totalSeconds = Convert.ToInt32(Double.Parse(reader.GetString().Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator) ?? ""));
		return new Time(totalSeconds);
	}

	public override void Write(Utf8JsonWriter writer, Time value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value.TotalSeconds);
	}
}