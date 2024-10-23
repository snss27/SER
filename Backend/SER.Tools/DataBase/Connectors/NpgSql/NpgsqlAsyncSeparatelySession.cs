using Npgsql;
using SER.Tools.DataBase.Sessions;
using System.Data;

namespace SER.Tools.DataBase.Connectors.NpgSql;

internal sealed class NpgsqlAsyncSeparatelySession : AsyncSeparatelySession<NpgsqlConnection, NpgsqlCommand, NpgsqlDataReader>
{
    public NpgsqlAsyncSeparatelySession(CancellationToken cancellationToken, NpgsqlConnection connection)
        : base(cancellationToken, connection) { }

    protected override Task<NpgsqlDataReader> ExecuteReader(NpgsqlCommand command, CommandBehavior commandBehavior)
    {
        return command.ExecuteReaderAsync(commandBehavior, _cancellationToken);
    }
}