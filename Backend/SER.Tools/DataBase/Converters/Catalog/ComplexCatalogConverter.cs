using SER.Tools.Json;
using System.Text.Json;

namespace SER.Tools.DataBase.Converters.Catalog;

internal static class ComplexCatalogConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        if (context.DataReader.GetFieldType(fieldIndex) == typeof(string))
        {
            string json = context.DataReader.GetString(fieldIndex).Trim();
            if (json.StartsWith('{')) json = "[" + json.TrimStart('{').TrimEnd('}') + "]";

            return JsonSerializer.Deserialize<T>(json, JsonTools.DefaultOptions) ?? default!;
        }

        throw new NotSupportedException($"Convert from {context.DataReader.GetFieldType(fieldIndex).Name} to {typeof(T).Name} not supported");
    }
}