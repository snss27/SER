using SER.Tools.DataBase.Converters.Catalog;
using SER.Tools.DataBase.Converters.Direct;
using SER.Tools.Types.Catalog;
using SER.Tools.Types.IDs;

namespace SER.Tools.DataBase.Converters;

internal static class ConverterFactory
{
    public static Convert<T> Create<T>(Type type)
    {
        if (type.IsEnum)
            return EnumConveter<T>.Convert;

        if (type == typeof(ID))
            return IdConverter<T>.Convert;

        if (type == typeof(DateTime))
            return DateTimeConverter<T>.Convert;

        if (type == typeof(DateOnly))
            return DateConverter<T>.Convert;

        if (type == typeof(TimeOnly))
            return TimeConverter<T>.Convert;

        if (type == typeof(TimeSpan))
            return TimeSpanConverter<T>.Convert;

        if (type == typeof(double))
            return DoubleConverter<T>.Convert;

        if (type == typeof(float))
            return SingleConverter<T>.Convert;

        if (type == typeof(ulong))
            return UInt64Converter<T>.Convert;

        if (type == typeof(uint))
            return UInt32Converter<T>.Convert;

        if (type == typeof(ushort))
            return UInt16Converter<T>.Convert;

        if (type == typeof(sbyte))
            return SByteConverter<T>.Convert;

        if (type.IsPrimitive || type == typeof(decimal) || type == typeof(string))
            return SimpleConverter<T>.Convert;

        if (Nullable.GetUnderlyingType(type) is Type underlyingType)
            return Create<T>(underlyingType);

        if (type == typeof(ID[]))
            return IdArrayConverter<T>.Convert;

        if (type.IsArray)
            return GetForCatalog<T>(type.GetElementType());


        if (type == typeof(Catalog<ID>))
            return IdCatalogConverter<T>.Convert;

        if (type == typeof(Catalog<string>) || type == typeof(Catalog<decimal>))
            return SimpleCatalogConverter<T>.Convert;

        if (type == typeof(Catalog<DateTime>) || type == typeof(Catalog<DateOnly>) || type == typeof(Catalog<TimeOnly>) || type == typeof(Catalog<TimeSpan>))
            return SimpleCatalogConverter<T>.Convert;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Catalog<>) && type.GenericTypeArguments[Index.Start] is Type elementType)
            return GetForCatalog<T>(elementType);


        // object, record or struct
        return ComplexConverter<T>.Convert;
    }

    private static Convert<T> GetForCatalog<T>(Type elementType)
    {
        if (elementType.IsEnum)
            return EnumCatalogConverter<T>.Convert;

        if (elementType.IsPrimitive)
            return SimpleCatalogConverter<T>.Convert;

        // json
        return ComplexCatalogConverter<T>.Convert;
    }



    public static R[] ConvertAll<T, R>(T[] input, Func<T, R> convert)
    {
        return Array.ConvertAll(input, convert.Invoke);
    }
}