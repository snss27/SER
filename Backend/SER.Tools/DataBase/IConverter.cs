using System.Data.Common;

namespace SER.Tools.DataBase;

public interface IConverter { }

public interface IConverter<T> : IConverter
{
    T Convert(DbDataReader dataReader, ref int offset);
}