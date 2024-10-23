using SER.Tools.DataBase.Query;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace SER.Tools.DataBase.Sessions;

internal abstract class SyncTransactionSession<T, C, D> : SyncSession<C, D>, ISyncTransactionSession
    where T : DbTransaction
    where C : DbCommand, new()
    where D : DbDataReader
{
    protected readonly T _transaction;

    protected SyncTransactionSession(T transaction)
    {
        if (transaction.Connection?.State is not ConnectionState.Open) throw new InvalidOperationException("Open connection before create session");

        _transaction = transaction;
    }

    protected override C BuildCommand(Query.Query query)
    {
        if (query is not Query<C> commandQuery) throw new ArgumentException("Session is not supported " + query.GetType().Name);

        C command = commandQuery.Build();

        command.Connection = _transaction.Connection;
        command.Transaction = _transaction;

        if (commandQuery.Preparable)
        {
            command.Prepare();
        }

        return command;
    }

    public override void Dispose()
    {
        if (Marshal.GetExceptionPointers() != nint.Zero)
        {
            try { _transaction.Rollback(); } catch { }
        }
        else
        {
            try { _transaction.Commit(); } catch { }
        }

        _transaction.Connection?.Dispose();
        _transaction.Dispose();
    }
}