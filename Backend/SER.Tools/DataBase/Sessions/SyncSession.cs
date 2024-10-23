using SER.Tools.DataBase.Converters;
using SER.Tools.Types.Pages;
using System.Data;
using System.Data.Common;

namespace SER.Tools.DataBase.Sessions;

internal abstract class SyncSession<C, D> : ISyncSeparatelySession
    where C : DbCommand
    where D : DbDataReader
{
    protected abstract C BuildCommand(Query.Query query);
    protected abstract D ExecuteReader(C command, CommandBehavior commandBehavior);
    public abstract void Dispose();

    public virtual int Execute(Query.Query query)
    {
        using C command = BuildCommand(query);

        return command.ExecuteNonQuery();
    }

    public virtual V Get<V>(Query.Query query)
    {
        using C command = BuildCommand(query);
        using D dataReader = ExecuteReader(command, CommandBehavior.SingleResult | CommandBehavior.SingleRow);

        if (dataReader.Read())
        {
            return ConverterContext.Get<V>(dataReader);
        }

        return default!;
    }

    public virtual V[] GetArray<V>(Query.Query query)
    {
        using C command = BuildCommand(query);
        using D dataReader = ExecuteReader(command, CommandBehavior.SingleResult);
        List<V> values = new();

        while (dataReader.Read())
        {
            ConverterContext.Add(values, dataReader);
        }

        return values.ToArray();
    }

    public virtual Dictionary<K, V> GetDictionary<K, V>(Query.Query query) where K : notnull
    {
        using C command = BuildCommand(query);
        using D dataReader = ExecuteReader(command, CommandBehavior.SingleResult);
        Dictionary<K, V> values = new();

        while (dataReader.Read())
        {
            ConverterContext.Add(values, dataReader);
        }

        return values;
    }

    public virtual Page<V> GetPage<V>(Query.Query query)
    {
        using C command = BuildCommand(query);
        using D dataReader = ExecuteReader(command, CommandBehavior.SingleResult);
        if (!dataReader.Read()) return Page.Empty;

        List<V> values = new();
        long totalCount = dataReader.GetInt64(dataReader.FieldCount - 1);

        ConverterContext.Add(values, dataReader);

        while (dataReader.Read())
        {
            ConverterContext.Add(values, dataReader);
        }

        return new Page<V>(values, totalCount);
    }

}