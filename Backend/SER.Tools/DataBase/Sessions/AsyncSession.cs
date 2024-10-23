using SER.Tools.DataBase.Converters;
using SER.Tools.Types.Pages;
using System.Data;
using System.Data.Common;

namespace SER.Tools.DataBase.Sessions;

internal abstract class AsyncSession<C, D> : IAsyncSeparatelySession
    where C : DbCommand
    where D : DbDataReader
{
    protected readonly CancellationToken _cancellationToken;

    protected AsyncSession(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    protected abstract Task<C> BuildCommand(Query.Query query);
    protected abstract Task<D> ExecuteReader(C command, CommandBehavior commandBehavior);
    public abstract ValueTask DisposeAsync();

    public virtual async Task<int> Execute(Query.Query query)
    {
        using C command = await BuildCommand(query);

        return await command.ExecuteNonQueryAsync(_cancellationToken);
    }

    public virtual async Task<V> Get<V>(Query.Query query)
    {
        using C command = await BuildCommand(query);
        using D dataReader = await ExecuteReader(command, CommandBehavior.SingleResult | CommandBehavior.SingleRow);

        if (await dataReader.ReadAsync(_cancellationToken))
        {
            return ConverterContext.Get<V>(dataReader);
        }

        return default!;
    }

    public virtual async Task<V[]> GetArray<V>(Query.Query query)
    {
        using C command = await BuildCommand(query);
        using D dataReader = await ExecuteReader(command, CommandBehavior.SingleResult);
        List<V> values = new();

        while (await dataReader.ReadAsync(_cancellationToken))
        {
            ConverterContext.Add(values, dataReader);
        }

        return values.ToArray();
    }

    public virtual async Task<Dictionary<K, V>> GetDictionary<K, V>(Query.Query query) where K : notnull
    {
        using C command = await BuildCommand(query);
        using D dataReader = await ExecuteReader(command, CommandBehavior.SingleResult);
        Dictionary<K, V> values = new();

        while (await dataReader.ReadAsync(_cancellationToken))
        {
            ConverterContext.Add(values, dataReader);
        }

        return values;
    }

    public virtual async Task<Page<V>> GetPage<V>(Query.Query query)
    {
        using C command = await BuildCommand(query);
        using D dataReader = await ExecuteReader(command, CommandBehavior.SingleResult);
        if (!await dataReader.ReadAsync(_cancellationToken)) return Page.Empty;

        List<V> values = new();
        long totalCount = dataReader.GetInt64(dataReader.FieldCount - 1);

        ConverterContext.Add(values, dataReader);

        while (await dataReader.ReadAsync(_cancellationToken))
        {
            ConverterContext.Add(values, dataReader);
        }

        return new Page<V>(values, totalCount);
    }
}