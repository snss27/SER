using SER.Tools.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SER.Tools.DataBase.Converters.Catalog;

/// <summary>Supports only enum with Int32 underline type</summary>
internal static class EnumCatalogConverter<T>
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

        return ConvertToGeneric(context.DataReader.GetFieldValue<int[]>(fieldIndex));
    }

    private static readonly Func<int[], T> ConvertToGeneric = Compile();

    private static Func<int[], T> Compile()
    {
        Type inputType = typeof(int[]);
        Type outputType = typeof(T);
        Type inputElementType = typeof(int);
        Type outputElementType = outputType.GenericTypeArguments[Index.Start];

        ParameterExpression inputElement = Expression.Parameter(inputElementType, nameof(inputElement));
        LambdaExpression elementConverter = Expression.Lambda(Expression.Convert(inputElement, outputElementType), inputElement);

        ParameterExpression inputArray = Expression.Parameter(inputType, nameof(inputArray));
        MethodInfo convertAll = typeof(ConverterFactory).GetMethod(nameof(ConverterFactory.ConvertAll))!.MakeGenericMethod(inputElementType, outputElementType);

        ConstructorInfo outputConstructor = outputType.GetConstructors()[Index.Start];

        return Expression.Lambda<Func<int[], T>>(Expression.New(outputConstructor, Expression.Call(convertAll, inputArray, elementConverter)), inputArray).Compile();
    }
}