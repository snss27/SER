using System.Data;

namespace SER.Tools.DataBase;

public interface IConnectorBase
{
    Query.Query CreateQuery(string sql, CommandType commandType = CommandType.Text, int? timeout = default, bool preparable = true);
}

public interface IAsyncConnector : IConnectorBase
{
    Task<IAsyncSeparatelySession> CreateAsyncSession(CancellationToken? cancellationToken = null, int? timeout = default);
    Task<IAsyncTransactionSession> CreateAsyncTransaction(CancellationToken? cancellationToken = null, int? timeout = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}

public interface ISyncConnector : IConnectorBase
{
    ISyncSeparatelySession CreateSession(int? timeout = default);
    ISyncTransactionSession CreateTransaction(int? timeout = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}

public interface IConnector : IAsyncConnector, ISyncConnector
{
    IConnectionString ConnectionString { get; }
}