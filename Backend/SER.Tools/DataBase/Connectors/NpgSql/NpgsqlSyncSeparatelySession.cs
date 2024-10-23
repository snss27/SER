using Npgsql;
using SER.Tools.DataBase.Sessions;
using System.Data;

namespace SER.Tools.DataBase.Connectors.NpgSql;

internal sealed class NpgsqlSyncSeparatelySession : SyncSeparatelySession<NpgsqlConnection, NpgsqlCommand, NpgsqlDataReader>
{
    public NpgsqlSyncSeparatelySession(NpgsqlConnection connection) : base(connection) { }

    protected override NpgsqlDataReader ExecuteReader(NpgsqlCommand command, CommandBehavior commandBehavior)
    {
        return command.ExecuteReader(commandBehavior);
    }
}