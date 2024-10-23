using SER.Tools.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SER.Tools.DataBase.Converters.Catalog;

internal static class SimpleCatalogConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        if (context.DataReader.GetFieldType(fieldIndex) == typeof(string))
        {
            string json = context.DataReader.GetString(fieldIndex).Trim();
            if (json is null or "") return default!;
            if (json.StartsWith('{')) json = "[" + json.TrimStart('{').TrimEnd('}') + "]";

            return JsonSerializer.Deserialize<T>(json, JsonTools.DefaultOptions) ?? default!;
        }

        return ConvertToGeneric(context.DataReader[fieldIndex]);
    }

    private static readonly Func<object, T> ConvertToGeneric = Compile();

    private static Func<object, T> Compile()
    {
        ParameterExpression parameter = Expression.Parameter(typeof(object));
        ConstructorInfo constructor = typeof(T).GetConstructors()[Index.Start];
        Type parameterType = constructor.GetParameters()[Index.Start].ParameterType;

        return Expression.Lambda<Func<object, T>>(Expression.New(constructor, Expression.Convert(parameter, parameterType)), parameter).Compile();
    }
}