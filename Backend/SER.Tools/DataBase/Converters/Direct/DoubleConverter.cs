namespace SER.Tools.DataBase.Converters.Direct;

internal static class DoubleConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        if (context.DataReader.GetFieldType(fieldIndex) == typeof(string)) return context.DataReader.GetString(fieldIndex).ToUpperInvariant() switch
        {
            "+INFINITY" or "INFINITY" => DirectGenericConverter<double?, T>.Convert(double.PositiveInfinity),
            "-INFINITY" => DirectGenericConverter<double?, T>.Convert(double.NegativeInfinity),
            "NAN" => DirectGenericConverter<double?, T>.Convert(double.NaN),
            string value => throw new InvalidCastException($"Convert '{value}' to Double? not supported"),
        };

        return DirectGenericConverter<double?, T>.Convert(context.DataReader.GetDouble(fieldIndex));
    }
}