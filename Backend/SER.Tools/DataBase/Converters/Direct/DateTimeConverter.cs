using System.Globalization;

namespace SER.Tools.DataBase.Converters.Direct;

internal static class DateTimeConverter<T>
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
            return CastGenericConverter<DateTime, T>.Convert(DateTime.Parse(context.DataReader.GetString(fieldIndex), CultureInfo.InvariantCulture));
        }

        return CastGenericConverter<DateTime, T>.Convert(context.DataReader.GetDateTime(fieldIndex));
    }
}