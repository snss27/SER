using System.Linq.Expressions;
using System.Reflection;

namespace SER.Tools.DataBase.Converters;

internal sealed class TypeInformation<T>
{
    public delegate void SetParameter(object?[] parameters, ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? name = null);
    public delegate void SetProperty(T instance, ref ConverterContext context, int fieldIndex = ConverterContext.FindFirstFieldIndex, string? name = null);

    public Func<object?[]?, object> Create { get; }
    public Dictionary<string, SetParameter>? Parameters { get; private set; }
    public Dictionary<string, SetProperty>? Properties { get; }

    public TypeInformation()
    {
        Type type = typeof(T);
        HashSet<string> propertyNamesViaConstructor = new(StringComparer.OrdinalIgnoreCase);
        Dictionary<string, PropertyInfo> writableProperties = new(StringComparer.OrdinalIgnoreCase);

        foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.CanWrite)
            {
                // public Int32 Property { get; set; }
                writableProperties[property.Name] = property;
            }
            else if (type.GetField($"<{property.Name}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance) is not null)
            {
                // public Int32 Property { get; }
                propertyNamesViaConstructor.Add(property.Name);
            }
            else
            {
                // public Int32 Property => 1;
            }
        }

        Create = FindConstructor(propertyNamesViaConstructor, writableProperties);

        if (writableProperties.Count > 0)
        {
            Properties = MakePropertySetters(writableProperties);
        }
    }

    private Func<object?[]?, object> FindConstructor(HashSet<string> propertyNamesViaConstructor, Dictionary<string, PropertyInfo> writableProperties)
    {
        Type type = typeof(T);
        ConstructorInfo? defaultConstructor = null;

        foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            ParameterInfo[] parameters = constructor.GetParameters();

            if (parameters.Length >= propertyNamesViaConstructor.Count)
            {
                HashSet<string>? writablePropertyNamesViaConstructor = GetPropertyNamesViaConstructor(propertyNamesViaConstructor, writableProperties, parameters);

                if (writablePropertyNamesViaConstructor is null)
                {
                    continue;
                }

                foreach (string writablePropertyNameViaConstructor in writablePropertyNamesViaConstructor)
                {
                    writableProperties.Remove(writablePropertyNameViaConstructor);
                }

                Parameters = MakeParameterSetters(parameters);

                return constructor.Invoke;
            }
            else if (parameters.Length is 0)
            {
                defaultConstructor = constructor;
            }
        }

        if (defaultConstructor is null)
        {
            string? typeNamespace = type.Namespace;
            if (typeNamespace is not null and not "") typeNamespace += ".";

            throw new NotSupportedException($"Type {typeNamespace}{type.Name} has no suitable constructor and not supported as return type in query");
        }

        return defaultConstructor.Invoke;
    }

    private static HashSet<string>? GetPropertyNamesViaConstructor(HashSet<string> propertyNamesViaConstructor, Dictionary<string, PropertyInfo> writableProperties, ParameterInfo[] parameters)
    {
        int propertyViaConstructorCount = 0;
        HashSet<string> writablePropertyNamesViaConstructor = new(StringComparer.OrdinalIgnoreCase);

        foreach (ParameterInfo parameter in parameters)
        {
            if (propertyNamesViaConstructor.Contains(parameter.Name!))
            {
                propertyViaConstructorCount++;
            }
            else if (writableProperties.ContainsKey(parameter.Name!))
            {
                writablePropertyNamesViaConstructor.Add(parameter.Name!);
            }
            else
            {
                return null;
            }
        }

        if (propertyViaConstructorCount < propertyNamesViaConstructor.Count)
        {
            return null;
        }

        return writablePropertyNamesViaConstructor;
    }

    private static Dictionary<string, SetProperty> MakePropertySetters(Dictionary<string, PropertyInfo> properties)
    {
        Dictionary<string, SetProperty> propertySetters = new(properties.Count, StringComparer.OrdinalIgnoreCase);

        foreach ((_, PropertyInfo propertyInfo) in properties)
        {
            ParameterExpression instance = Expression.Parameter(typeof(T), nameof(instance));
            ParameterExpression converterContext = Expression.Parameter(typeof(ConverterContext).MakeByRefType(), nameof(converterContext));
            ParameterExpression position = Expression.Parameter(typeof(int), nameof(position));
            ParameterExpression name = Expression.Parameter(typeof(string), nameof(name));

            MemberExpression property = Expression.Property(instance, propertyInfo);
            BinaryExpression body = Expression.Assign(property, Convert(propertyInfo.PropertyType, converterContext, position, name));
            SetProperty setProperty = Expression.Lambda<SetProperty>(body, instance, converterContext, position, name).Compile().Invoke;

            propertySetters.Add(propertyInfo.Name, setProperty);
        }

        return propertySetters;
    }

    private static Dictionary<string, SetParameter>? MakeParameterSetters(ParameterInfo[] parameters)
    {
        if (parameters.Length is 0) return null;

        Dictionary<string, SetParameter> parameterSetters = new(parameters.Length, StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < parameters.Length; i++)
        {
            ParameterInfo parameter = parameters[i];

            ParameterExpression array = Expression.Parameter(typeof(object?[]), nameof(array));
            ParameterExpression converterContext = Expression.Parameter(typeof(ConverterContext).MakeByRefType(), nameof(converterContext));
            ParameterExpression position = Expression.Parameter(typeof(int), nameof(position));
            ParameterExpression name = Expression.Parameter(typeof(string), nameof(name));

            IndexExpression element = Expression.ArrayAccess(array, Expression.Constant(i));
            BinaryExpression body = Expression.Assign(element, Expression.Convert(Convert(parameter.ParameterType, converterContext, position, name), typeof(object)));
            SetParameter setParameter = Expression.Lambda<SetParameter>(body, array, converterContext, position, name).Compile().Invoke;

            parameterSetters.Add(parameter.Name!, setParameter);
        }

        return parameterSetters;
    }

    private static InvocationExpression Convert(Type type, ParameterExpression converterContext, ParameterExpression offset, ParameterExpression name)
    {
        PropertyInfo converter = typeof(Converter<>).MakeGenericType(type).GetProperty(nameof(Converter<Int32>.Convert))!;

        return Expression.Invoke(Expression.Property(null, converter), converterContext, offset, name);
    }
}