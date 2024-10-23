using SER.Tools.DataBase.Query;
using System.Data;
using System.Data.Common;

namespace SER.Tools.DataBase.Sessions;

internal abstract class SyncSeparatelySession<T, C, D> : SyncSession<C, D>
    where T : DbConnection, new()
    where C : DbCommand, new()
    where D : DbDataReader
{
    protected readonly T _connection;

    protected SyncSeparatelySession(T connection)
    {
        if (connection.State is not ConnectionState.Open) throw new InvalidOperationException("Open connection before create session");

        _connection = connection;
    }

    protected override C BuildCommand(Query.Query query)
    {
        if (query is not Query<C> commandQuery) throw new ArgumentException("Session is not supported " + query.GetType().Name);

        C command = commandQuery.Build();

        command.Connection = _connection;

        if (commandQuery.Preparable)
        {
            command.Prepare();
        }

        return command;
    }

    public override void Dispose()
    {
        _connection.Dispose();
    }
}