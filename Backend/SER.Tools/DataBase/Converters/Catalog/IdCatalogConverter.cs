using SER.Tools.Json;
using SER.Tools.Types.IDs;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SER.Tools.DataBase.Converters.Catalog;

internal static class IdCatalogConverter<T>
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

        return ConvertToGeneric(context.DataReader.GetFieldValue<byte[][]>(fieldIndex));
    }

    private static readonly Func<byte[][], T> ConvertToGeneric = Compile();

    private static Func<byte[][], T> Compile()
    {
        Type inputType = typeof(byte[][]);
        Type outputType = typeof(T);
        Type inputElementType = typeof(byte[]);
        Type outputElementType = outputType.GenericTypeArguments[Index.Start];

        ConstructorInfo idConstructor = typeof(ID).GetConstructor(new[] { inputElementType })!;
        ParameterExpression inputElement = Expression.Parameter(inputElementType, nameof(inputElement));
        LambdaExpression elementConverter = Expression.Lambda(Expression.New(idConstructor, inputElement), inputElement);

        ParameterExpression inputArray = Expression.Parameter(inputType, nameof(inputArray));
        MethodInfo convertAll = typeof(ConverterFactory).GetMethod(nameof(ConverterFactory.ConvertAll))!.MakeGenericMethod(inputElementType, outputElementType);

        ConstructorInfo outputConstructor = outputType.GetConstructors()[Index.Start];

        return Expression.Lambda<Func<byte[][], T>>(Expression.New(outputConstructor, Expression.Call(convertAll, inputArray, elementConverter)), inputArray).Compile();
    }
}