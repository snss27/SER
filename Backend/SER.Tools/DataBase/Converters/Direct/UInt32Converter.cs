namespace SER.Tools.DataBase.Converters.Direct;

internal static class UInt32Converter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        long value = context.DataReader.GetInt64(fieldIndex);

        if (value is < uint.MinValue or > uint.MaxValue)
        {
            throw new OverflowException($"Convert {value} to UInt32? not supported");
        }

        return CastGenericConverter<long, T>.Convert(value);
    }
}