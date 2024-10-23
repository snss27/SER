namespace SER.Tools.DataBase.Converters.Direct;

internal static class SingleConverter<T>
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
            "+INFINITY" or "INFINITY" => DirectGenericConverter<float?, T>.Convert(float.PositiveInfinity),
            "-INFINITY" => DirectGenericConverter<float?, T>.Convert(float.NegativeInfinity),
            "NAN" => DirectGenericConverter<float?, T>.Convert(float.NaN),
            string value => throw new InvalidCastException($"Convert '{value}' to Single? not supported"),
        };

        return DirectGenericConverter<float?, T>.Convert(context.DataReader.GetFloat(fieldIndex));
    }
}