namespace SER.Tools.DataBase.Converters;

public delegate T Convert<T>(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? name = null);