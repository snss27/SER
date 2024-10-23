using SER.Tools.DataBase.Query;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace SER.Tools.DataBase.Sessions;

internal abstract class AsyncTransactionSession<T, C, D> : AsyncSession<C, D>, IAsyncTransactionSession
    where T : DbTransaction
    where C : DbCommand, new()
    where D : DbDataReader
{
    protected readonly T _transaction;

    protected AsyncTransactionSession(CancellationToken cancellationToken, T transaction) : base(cancellationToken)
    {
        if (transaction.Connection?.State is not ConnectionState.Open) throw new InvalidOperationException("Open connection before create session");

        _transaction = transaction;
    }

    protected override async Task<C> BuildCommand(Query.Query query)
    {
        if (query is not Query<C> commandQuery) throw new ArgumentException("Session is not supported " + query.GetType().Name);

        C command = commandQuery.Build();

        command.Connection = _transaction.Connection;
        command.Transaction = _transaction;

        if (commandQuery.Preparable)
        {
            await command.PrepareAsync(_cancellationToken);
        }

        return command;
    }

    public override async ValueTask DisposeAsync()
    {
        if (Marshal.GetExceptionPointers() != nint.Zero)
        {
            try { await _transaction.RollbackAsync(_cancellationToken); } catch { }
        }
        else
        {
            try { await _transaction.CommitAsync(_cancellationToken); } catch { }
        }

        if (_transaction.Connection is not null)
        {
            await _transaction.Connection.DisposeAsync();
        }

        await _transaction.DisposeAsync();
    }
}