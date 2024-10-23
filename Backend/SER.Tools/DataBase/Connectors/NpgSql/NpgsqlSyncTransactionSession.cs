using Npgsql;
using SER.Tools.DataBase.Sessions;
using System.Data;

namespace SER.Tools.DataBase.Connectors.NpgSql;

internal sealed class NpgsqlSyncTransactionSession : SyncTransactionSession<NpgsqlTransaction, NpgsqlCommand, NpgsqlDataReader>
{
    public NpgsqlSyncTransactionSession(NpgsqlTransaction transaction) : base(transaction) { }

    protected override NpgsqlDataReader ExecuteReader(NpgsqlCommand command, CommandBehavior commandBehavior)
    {
        return command.ExecuteReader(commandBehavior);
    }
}