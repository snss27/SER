namespace SER.Tools.DataBase.Converters.Direct;

internal static class SByteConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        short value = context.DataReader.GetInt16(fieldIndex);

        if (value is < sbyte.MinValue or > sbyte.MaxValue)
        {
            throw new OverflowException($"Convert {value} to SByte not supported");
        }

        return CastGenericConverter<short, T>.Convert(value);
    }
}