using Npgsql;
using SER.Tools.Converters;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public class NpgSqlConnector : IConnector
{
    public IConnectionString ConnectionString { get; }

    private readonly IJsonSerializer _jsonSerializer;

    public NpgSqlConnector(IConnectionString connectionString, IJsonSerializer jsonSerializer)
    {
        ConnectionString = connectionString;
        _jsonSerializer = jsonSerializer;
    }

    public Query.Query CreateQuery(string sql, CommandType commandType = CommandType.Text, int? timeout = default, bool preparable = true)
    {
        return new NpgSqlQuery(sql, commandType, timeout, preparable, _jsonSerializer);
    }

    public ISyncSeparatelySession CreateSession(int? timeout = default)
    {
        NpgsqlConnection connection = new(MakeConnectionString(timeout));
        connection.Open();

        return new NpgsqlSyncSeparatelySession(connection);
    }

    public async Task<IAsyncSeparatelySession> CreateAsyncSession(CancellationToken? cancellationToken = null, int? timeout = default)
    {
        if (cancellationToken is null) cancellationToken = new CancellationToken();

        NpgsqlConnection connection = new(MakeConnectionString(timeout));
        await connection.OpenAsync(cancellationToken.Value);

        return new NpgsqlAsyncSeparatelySession(cancellationToken.Value, connection);
    }

    [SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>")]
    public ISyncTransactionSession CreateTransaction(int? timeout = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        NpgsqlConnection connection = new(MakeConnectionString(timeout));
        connection.Open();

        NpgsqlTransaction transaction = connection.BeginTransaction(isolationLevel);

        return new NpgsqlSyncTransactionSession(transaction);
    }

    [SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>")]
    public async Task<IAsyncTransactionSession> CreateAsyncTransaction(CancellationToken? cancellationToken = null, int? timeout = default, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
    {
        if (cancellationToken is null) cancellationToken = new CancellationToken();

        NpgsqlConnection connection = new(MakeConnectionString(timeout));
        await connection.OpenAsync(cancellationToken.Value);

        NpgsqlTransaction transaction = await connection.BeginTransactionAsync(isolationLevel, cancellationToken.Value);

        return new NpgsqlAsyncTransactionSession(cancellationToken.Value, transaction);
    }

    private string MakeConnectionString(int? timeout)
    {
        if (timeout.HasValue)
        {
            return new NpgsqlConnectionString(ConnectionString.Value, timeout.Value).Value;
        }
        else
        {
            return ConnectionString.Value;
        }
    }

    public override string ToString()
    {
        return $"{nameof(NpgSqlConnector)} ({ConnectionString})";
    }
}