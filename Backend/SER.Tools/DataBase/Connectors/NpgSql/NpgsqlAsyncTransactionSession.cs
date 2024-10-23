using Npgsql;
using SER.Tools.DataBase.Sessions;
using System.Data;

namespace SER.Tools.DataBase.Connectors.NpgSql;

internal sealed class NpgsqlAsyncTransactionSession : AsyncTransactionSession<NpgsqlTransaction, NpgsqlCommand, NpgsqlDataReader>
{
    public NpgsqlAsyncTransactionSession(CancellationToken cancellationToken, NpgsqlTransaction transaction)
        : base(cancellationToken, transaction) { }

    protected override Task<NpgsqlDataReader> ExecuteReader(NpgsqlCommand command, CommandBehavior commandBehavior)
    {
        return command.ExecuteReaderAsync(commandBehavior, _cancellationToken);
    }
}