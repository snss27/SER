using System.Globalization;

namespace SER.Tools.DataBase.Converters.Direct;

internal static class DateConverter<T>
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
            return CastGenericConverter<DateOnly, T>.Convert(DateOnly.Parse(context.DataReader.GetString(fieldIndex), CultureInfo.InvariantCulture));
        }

        if (context.DataReader.GetFieldType(fieldIndex) == typeof(DateTime))
        {
            return CastGenericConverter<DateOnly, T>.Convert(DateOnly.FromDateTime(context.DataReader.GetDateTime(fieldIndex)));
        }

        return CastGenericConverter<DateOnly, T>.Convert(context.DataReader.GetFieldValue<DateOnly>(fieldIndex));
    }
}