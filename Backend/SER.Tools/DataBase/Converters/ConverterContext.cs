using System.Data.Common;

namespace SER.Tools.DataBase.Converters;

public ref struct ConverterContext
{
    public const int FindFirstFieldIndex = -1;

    public static V Get<V>(DbDataReader dataReader)
    {
        ConverterContext converterContext = new(dataReader);

        return Converter<V>.Convert(ref converterContext);
    }

    public static void Add<V>(List<V> values, DbDataReader dataReader)
    {
        ConverterContext converterContext = new(dataReader);

        values.Add(Converter<V>.Convert(ref converterContext));
    }

    public static void Add<K, V>(Dictionary<K, V> values, DbDataReader dataReader) where K : notnull
    {
        ConverterContext converterContext = new(dataReader);

        values.Add(Converter<K>.Convert(ref converterContext), Converter<V>.Convert(ref converterContext));
    }

    public DbDataReader DataReader { get; }
    private byte[] _usedFields;

    public ConverterContext(DbDataReader dataReader)
    {
        DataReader = dataReader;
        _usedFields = new byte[dataReader.FieldCount];
    }

    public readonly bool NextFieldIndex(ref int fieldIndex)
    {
        for (int i = fieldIndex + 1; i < _usedFields.Length; i++)
        {
            if (_usedFields[i] is 1) continue;

            fieldIndex = i;

            return true;
        }

        return false;
    }

    public int TakeFieldIndex(int fieldIndex)
    {
        if (fieldIndex is FindFirstFieldIndex && !NextFieldIndex(ref fieldIndex))
        {
            throw new KeyNotFoundException($"Field by offset {fieldIndex} is used or not found");
        }

        _usedFields[fieldIndex] = 1;

        return fieldIndex;
    }

    public override string ToString()
    {
        return "0b" + string.Concat(_usedFields);
    }
}