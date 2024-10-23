using SER.Tools.Types.Catalog;

namespace SER.Tools.DataBase.Query.GenericParameters;

internal static class Parameter<T>
{
    public static AddParameter<T> Add { get; set; } = Create(typeof(T));

    private static AddParameter<T> Create(Type type)
    {
        if (type.IsEnum)
            return EnumParameter<T>.Add;

        if (Nullable.GetUnderlyingType(type) is Type undelineType)
            return Create(undelineType);

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Catalog<>) && type.GenericTypeArguments[Index.Start] is Type elementType)
        {
            if (elementType.IsEnum)
                return EnumCatalogParameter<T>.Add;

            // object, record or struct
            return AddJson;
        }

        // object, record or struct
        return AddJson;
    }

    private static IParameter AddJson(Query query, T value, string name)
    {
        return query.AddJson(value, name);
    }
}