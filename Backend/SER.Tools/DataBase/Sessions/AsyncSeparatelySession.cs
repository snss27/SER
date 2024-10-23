using SER.Tools.DataBase.Query;
using System.Data;
using System.Data.Common;

namespace SER.Tools.DataBase.Sessions;

internal abstract class AsyncSeparatelySession<T, C, D> : AsyncSession<C, D>
    where T : DbConnection, new()
    where C : DbCommand, new()
    where D : DbDataReader
{
    protected readonly T _connection;

    protected AsyncSeparatelySession(CancellationToken cancellationToken, T connection) : base(cancellationToken)
    {
        if (connection.State is not ConnectionState.Open) throw new InvalidOperationException("Open connection before create session");

        _connection = connection;
    }

    protected override async Task<C> BuildCommand(Query.Query query)
    {
        if (query is not Query<C> commandQuery) throw new ArgumentException("Session is not supported " + query.GetType().Name);

        C command = commandQuery.Build();

        command.Connection = _connection;

        if (commandQuery.Preparable)
        {
            await command.PrepareAsync(_cancellationToken);
        }

        return command;
    }

    public override ValueTask DisposeAsync()
    {
        return _connection.DisposeAsync();
    }
}