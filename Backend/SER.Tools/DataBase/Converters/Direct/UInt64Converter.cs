namespace SER.Tools.DataBase.Converters.Direct;

internal static class UInt64Converter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        decimal value = context.DataReader.GetDecimal(fieldIndex);

        if (value is < ulong.MinValue or > ulong.MaxValue)
        {
            throw new OverflowException($"Convert {value} to UInt64? not supported");
        }

        return CastGenericConverter<decimal, T>.Convert(value);
    }
}