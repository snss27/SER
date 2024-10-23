using SER.Tools.Types.Catalog;
using System.Linq.Expressions;
using System.Reflection;

namespace SER.Tools.DataBase.Query.GenericParameters;

/// <summary>Wrong values do not check</summary>
internal static class EnumCatalogParameter<T>
{
    public static AddParameter<T> Add = Compile();

    // Example: Add(quert, Catalog<DayOfWeek?>? value, String name)
    private static AddParameter<T> Compile()
    {
        // Example: Catalog<DayOfWeek?>, DayOfWeek?, Catalog<Int32?>, Int32?
        GetTypes(out Type sourceEnumCatalogType, out Type sourceEnumElementType, out Type targetValueCatalogType, out Type targetValueElementType);

        // Example: Query query
        ParameterExpression query = Expression.Parameter(typeof(Query));
        // Example: Catalog<DayOfWeek?>? value
        ParameterExpression enumCatalogValue = Expression.Parameter(sourceEnumCatalogType);
        // Example: String name
        ParameterExpression name = Expression.Parameter(typeof(string));
        // Example: Query.Add<Catalog<Int32?>>(value, name)
        MethodCallExpression addParameter = AddParameter(query, enumCatalogValue, name, sourceEnumCatalogType, sourceEnumElementType, targetValueCatalogType, targetValueElementType);

        return Expression.Lambda<AddParameter<T>>(addParameter, query, enumCatalogValue, name).Compile();
    }

    // Example: Catalog<DayOfWeek?>, DayOfWeek?, Catalog<Int32?>, Int32?
    private static void GetTypes(out Type sourceEnumCatalogType, out Type sourceEnumElementType, out Type targetValueCatalogType, out Type targetValueElementType)
    {
        // Example: Catalog<DayOfWeek?>?
        sourceEnumCatalogType = typeof(T);
        // Example: Catalog<DayOfWeek?>
        Type? enumCatalogType = Nullable.GetUnderlyingType(sourceEnumCatalogType);

        // Example: DayOfWeek?
        sourceEnumElementType = (enumCatalogType ?? sourceEnumCatalogType).GetGenericArguments()[Index.Start];
        // Example: DayOfWeek
        Type? enumElementType = Nullable.GetUnderlyingType(sourceEnumElementType);

        // Example: Int32
        Type valueElementType = Enum.GetUnderlyingType(enumElementType ?? sourceEnumElementType);
        // Example: Int32?
        targetValueElementType = enumElementType is null ? valueElementType : typeof(Nullable<>).MakeGenericType(valueElementType);

        // Example: Catalog<Int32?>?
        targetValueCatalogType = typeof(Catalog<>).MakeGenericType(targetValueElementType);
    }

    // Example: query.Add(ConverterFactory.ConvertAll<DayOfWeek?, Int32?>(value), name)
    private static MethodCallExpression AddParameter(ParameterExpression query, ParameterExpression enumCatalogValue, ParameterExpression name, Type sourceEnumCatalogType, Type sourceEnumElementType, Type targetValueCatalogType, Type targetValueElementType)
    {
        // Example: values.Convert(value => (Int32?)value)
        MethodCallExpression convert = ConvertEnumCatalogToValueCatalog(enumCatalogValue, sourceEnumCatalogType, sourceEnumElementType, targetValueElementType);
        // Example: Query.Add<Catalog<Int32?>>(value, name)
        MethodInfo? addParameter = typeof(Query).GetMethod(nameof(Query.Add), new[] { targetValueCatalogType, typeof(string) });
        if (addParameter is null) throw new EntryPointNotFoundException($"Method {nameof(Query)}.{nameof(Query.Add)}({targetValueCatalogType.Name} value, String name) is not found");

        return Expression.Call(query, addParameter, convert, name);
    }

    // Example: values.Convert(value => (Int32?)value)
    private static MethodCallExpression ConvertEnumCatalogToValueCatalog(ParameterExpression enumCatalogValue, Type sourceEnumCatalogType, Type sourceEnumElementType, Type targetValueElementType)
    {
        // Example: DayOfWeek? enumElement
        ParameterExpression inputEnumElement = Expression.Parameter(sourceEnumElementType, nameof(inputEnumElement));
        // Example: enumElement => (Int32?)enumElement
        LambdaExpression elementConverter = Expression.Lambda(Expression.Convert(inputEnumElement, targetValueElementType), inputEnumElement);

        // Example: values.Convert(elementConverter)
        MethodInfo? convertAll = sourceEnumCatalogType.GetMethod(nameof(Catalog<T>.Convert))?.MakeGenericMethod(targetValueElementType);
        if (convertAll is null) throw new EntryPointNotFoundException($"Method {nameof(Catalog)}<{sourceEnumElementType.Name}>.{nameof(Catalog<T>.Convert)}(Func<{sourceEnumElementType.Name},{targetValueElementType.Name}> converter) is not found");

        // Example: ConverterFactory.ConvertAll<DayOfWeek?, Int32?>(value)
        return Expression.Call(enumCatalogValue, convertAll, elementConverter);
    }
}