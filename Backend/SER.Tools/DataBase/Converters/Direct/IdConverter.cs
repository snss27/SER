using SER.Tools.Types.IDs;
using System.Linq.Expressions;
using System.Reflection;

namespace SER.Tools.DataBase.Converters.Direct;

internal static class IdConverter<T>
{
    public static T Convert(ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? _ = null)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        return ConvertToGeneric(context.DataReader.GetFieldValue<byte[]>(fieldIndex));
    }

    private static readonly Func<byte[], T> ConvertToGeneric = Compile();

    private static Func<byte[], T> Compile()
    {
        ParameterExpression parameter = Expression.Parameter(typeof(byte[]));
        ConstructorInfo constructor = typeof(ID).GetConstructor(new[] { typeof(byte[]) })!;

        return Expression.Lambda<Func<byte[], T>>(Expression.Convert(Expression.New(constructor, parameter), typeof(T)), parameter).Compile();
    }
}