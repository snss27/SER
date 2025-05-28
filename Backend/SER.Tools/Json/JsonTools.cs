using SER.Tools.Json.Converters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SER.Tools.Json;

public static class JsonTools
{
	public static JsonSerializerOptions DefaultOptions { get; set; } = AddJsonSettings(new());

	public static JsonSerializerOptions Options = new();

	private static readonly JsonConverter[] Converters =
	{
		new IDJsonConverter()
	};

	public static JsonSerializerOptions AddJsonSettings(this JsonSerializerOptions options)
	{
		options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

		Options = options;
		return options;
	}

	public static JsonSerializerOptions ApplyToolsConverters(this JsonSerializerOptions options)
	{
		foreach (JsonConverter converter in Converters)
			options.Converters.Add(converter);

		Options = options;
		return options;
	}

	public static JsonSerializerOptions ApplyAnyTypeConverters(this JsonSerializerOptions options, Assembly forAssembly)
	{
		options.Converters.Add(new AnyTypeJsonConverterFactory(forAssembly));

		Options = options;
		return options;
	}

	public static String Serialize(this Object @object)
	{
		if (@object is null) return null;
		return JsonSerializer.Serialize(@object, Options);
	}

	public static T? Deserialize<T>(this String @string)
	{
		if (@string is null) return default;
		return JsonSerializer.Deserialize<T>(@string, Options);
	}
}
