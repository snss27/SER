using System.Globalization;

namespace SER.Tools.DataBase.Converters.Direct;

internal static class TimeSpanConverter<T>
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
            return CastGenericConverter<TimeSpan, T>.Convert(TimeSpan.Parse(context.DataReader.GetString(fieldIndex), CultureInfo.InvariantCulture));
        }

        return CastGenericConverter<TimeSpan, T>.Convert(context.DataReader.GetFieldValue<TimeSpan>(fieldIndex));
    }
}