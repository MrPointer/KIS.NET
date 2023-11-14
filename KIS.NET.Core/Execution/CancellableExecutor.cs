using System;
using System.Threading;
using System.Threading.Tasks;

namespace KIS.NET.Core.Execution
{
    /// <summary>
    /// A class used to execute actions or functions in a separate <see cref="T:System.Threading.Tasks.Task" />
    /// with the ability to cancel them using a cancellation token as well.
    /// </summary>
    public class CancellableExecutor : Executor, ICancellableExecutor<CancellationToken>
    {
        public Task<TResult> Execute<TResult>(Func<CancellationToken, TResult> function,
            CancellationToken cancellationToken)
        {
            return Task.Run(() => function(cancellationToken), cancellationToken);
        }

        Task ICancellableExecutor<CancellationToken>.Execute(Action<CancellationToken> action,
            CancellationToken cancellationToken)
        {
            return Task.Run(() => action(cancellationToken), cancellationToken);
        }
    }
}