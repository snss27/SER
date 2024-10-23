using SER.Tools.Types.Pages;

namespace SER.Tools.DataBase;

public interface ISyncSession
{
    /// <summary>Executes a query against a connection and returns number of rows affected.</summary>
    int Execute(Query.Query query);
    V Get<V>(Query.Query query);
    V[] GetArray<V>(Query.Query query);
    Dictionary<K, V> GetDictionary<K, V>(Query.Query query) where K : notnull;
    Page<V> GetPage<V>(Query.Query query);
}

public interface ISyncSeparatelySession : ISyncSession, IDisposable { }
public interface ISyncTransactionSession : ISyncSession, IDisposable { }

public interface IAsyncSession
{
    /// <summary>Executes a query against a connection and returns number of rows affected.</summary>
    Task<int> Execute(Query.Query query);
    Task<V> Get<V>(Query.Query query);
    Task<V[]> GetArray<V>(Query.Query query);
    Task<Dictionary<K, V>> GetDictionary<K, V>(Query.Query query) where K : notnull;
    Task<Page<V>> GetPage<V>(Query.Query query);
}

public interface IAsyncSeparatelySession : IAsyncSession, IAsyncDisposable { }
public interface IAsyncTransactionSession : IAsyncSession, IAsyncDisposable { }