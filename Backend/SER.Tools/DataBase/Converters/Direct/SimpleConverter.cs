namespace SER.Tools.DataBase.Converters.Direct;

internal static class SimpleConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        return context.DataReader.GetFieldValue<T>(fieldIndex);
    }
}