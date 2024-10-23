using SER.Tools.Json;
using System.Text.Json;

namespace SER.Tools.DataBase.Converters.Direct;

/// <summary>Class must have default or constructor with parameters for all non-writable properties</summary>
internal static class ComplexConverter<T>
{
    private static readonly TypeInformation<T> TypeInfo = new();

    public static T Convert(ref ConverterContext context, int startFieldIndex = ConverterContext.FindFirstFieldIndex, string? name = null)
    {
        Dictionary<string, (int FieldIndex, TypeInformation<T>.SetProperty SetProperty)>? properties = null;
        object?[]? parameters = null;
        int fieldIndex = ConverterContext.FindFirstFieldIndex;

        while (context.NextFieldIndex(ref fieldIndex))
        {
            string fieldName = context.DataReader.GetName(fieldIndex);
            string propertyName = fieldName;

            if (name is not null)
            {
                if (!fieldName.StartsWith(name, StringComparison.OrdinalIgnoreCase)) continue;

                propertyName = fieldName[name.Length..];
            }

            // json
            if (propertyName is "")
            {
                return ConvertFromJson(ref context, fieldIndex);
            }

            // struct, record, class
            if (TypeInfo.Parameters is not null && TypeInfo.Parameters.TryGetValue(propertyName, out TypeInformation<T>.SetParameter? setParameter))
            {
                parameters ??= new object?[TypeInfo.Parameters.Count];
                setParameter(parameters, ref context, fieldIndex, name + propertyName);
            }
            else if (TypeInfo.Properties is not null && TypeInfo.Properties.TryGetValue(propertyName, out TypeInformation<T>.SetProperty? setProperty))
            {
                properties ??= new(TypeInfo.Properties.Count);
                properties.TryAdd(name + propertyName, (fieldIndex, setProperty));
            }
            else if (TypeInfo.Parameters?.FirstOrDefault(ParameterStartsWithFieldName) is { Key: string parameter, Value: TypeInformation<T>.SetParameter setPossibleParameter })
            {
                parameters ??= new object?[TypeInfo.Parameters.Count];
                setPossibleParameter(parameters, ref context, fieldIndex, name + parameter);
            }
            else if (TypeInfo.Properties?.FirstOrDefault(PropertyStartsWithFieldName) is { Key: string property, Value: TypeInformation<T>.SetProperty setPossibleProperty })
            {
                properties ??= new(TypeInfo.Properties.Count);
                properties.TryAdd(name + property, (fieldIndex, setPossibleProperty));
            }

            bool ParameterStartsWithFieldName(KeyValuePair<string, TypeInformation<T>.SetParameter> parameter)
            {
                return propertyName.StartsWith(parameter.Key, StringComparison.OrdinalIgnoreCase);
            }

            bool PropertyStartsWithFieldName(KeyValuePair<string, TypeInformation<T>.SetProperty> property)
            {
                return propertyName.StartsWith(property.Key, StringComparison.OrdinalIgnoreCase);
            }
        }

        if (parameters is null && TypeInfo.Parameters is not null)
        {
            return ConvertFromJson(ref context, startFieldIndex);
        }

        T instance = (T)TypeInfo.Create(parameters);

        if (properties is not null)
        {
            foreach (KeyValuePair<string, (int FieldIndex, TypeInformation<T>.SetProperty SetProperty)> property in properties)
            {
                property.Value.SetProperty(instance, ref context, property.Value.FieldIndex, property.Key);
            }
        }

        return instance;
    }

    private static T ConvertFromJson(ref ConverterContext context, int fieldIndex)
    {
        fieldIndex = context.TakeFieldIndex(fieldIndex);

        if (context.DataReader.IsDBNull(fieldIndex))
        {
            return default!;
        }

        string json = context.DataReader.GetString(fieldIndex);

        if (json is null or "")
        {
            return default!;
        }

        return JsonSerializer.Deserialize<T>(json, JsonTools.DefaultOptions) ?? default!;
    }
}