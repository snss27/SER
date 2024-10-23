namespace SER.Tools.DataBase.Converters;

internal static class Converter<T>
{
    public static Convert<T> Convert { get; set; } = ConverterFactory.Create<T>(typeof(T));
}