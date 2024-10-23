namespace SER.Tools.DataBase.Converters.Direct;

/// <summary>Wrong values do not check</summary>
internal static class EnumConveter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        return CastGenericConverter<long, T>.Convert(context.DataReader.GetInt64(fieldIndex));
    }
}