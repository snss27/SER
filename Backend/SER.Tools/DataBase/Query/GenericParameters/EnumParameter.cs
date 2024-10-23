using System.Linq.Expressions;
using System.Reflection;

namespace SER.Tools.DataBase.Query.GenericParameters;

/// <summary>Wrong values do not check</summary>
internal static class EnumParameter<T>
{
    public static AddParameter<T> Add = Compile();

    // Example: Add(quert, DayOfWeek? value, String name)
    private static AddParameter<T> Compile()
    {
        // Example: DayOfWeek?, Int32?
        GetTypes(out Type sourceEnumType, out Type targetValueType);

        // Example: Query query
        ParameterExpression query = Expression.Parameter(typeof(Query));
        // Example: DayOfWeek? value
        ParameterExpression enumValue = Expression.Parameter(sourceEnumType);
        // Example: String name
        ParameterExpression name = Expression.Parameter(typeof(string));
        // Example: query.Add((Int32?)value, name)
        MethodCallExpression addParameter = AddParameter(query, enumValue, name, targetValueType);

        return Expression.Lambda<AddParameter<T>>(addParameter, query, enumValue, name).Compile();
    }

    // Example: DayOfWeek?, Int32?
    private static void GetTypes(out Type sourceEnumType, out Type targetValueType)
    {
        // Example: DayOfWeek?
        sourceEnumType = typeof(T);
        // Example: DayOfWeek
        Type? enumType = Nullable.GetUnderlyingType(sourceEnumType);

        // Example: Int32
        Type valueType = Enum.GetUnderlyingType(enumType ?? sourceEnumType);

        // Example: Int32?
        targetValueType = enumType is null ? valueType : typeof(Nullable<>).MakeGenericType(valueType);
    }

    // Example: query.Add((Int32?)value, name)
    private static MethodCallExpression AddParameter(ParameterExpression query, ParameterExpression enumValue, ParameterExpression name, Type targetValueType)
    {
        // Example: (Int32?)value
        UnaryExpression convert = Expression.Convert(enumValue, targetValueType);
        // Example: Query.Add<Int32?>(value, name)
        MethodInfo? addParameter = typeof(Query).GetMethod(nameof(Query.Add), new[] { targetValueType, typeof(string) });
        if (addParameter is null) throw new EntryPointNotFoundException($"Method {nameof(Query)}.{nameof(Query.Add)}({targetValueType.Name} value, String name) is not found");

        return Expression.Call(query, addParameter, convert, name);
    }
}