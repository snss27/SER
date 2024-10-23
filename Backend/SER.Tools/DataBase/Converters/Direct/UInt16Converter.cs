namespace SER.Tools.DataBase.Converters.Direct;

internal static class UInt16Converter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        int value = context.DataReader.GetInt32(fieldIndex);

        if (value is < ushort.MinValue or > ushort.MaxValue)
        {
            throw new OverflowException($"Convert {value} to UInt16 not supported");
        }

        return CastGenericConverter<int, T>.Convert(value);
    }
}