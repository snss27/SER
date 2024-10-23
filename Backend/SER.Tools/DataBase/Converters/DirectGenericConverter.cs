using System.Linq.Expressions;

namespace SER.Tools.DataBase.Converters;

internal static class DirectGenericConverter<T, R>
{
    public static Func<T, R> Convert { get; } = Compile();

    public static Func<T, R> Compile()
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T));

        return Expression.Lambda<Func<T, R>>(parameter, parameter).Compile();
    }
}