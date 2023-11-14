using System;
using System.Threading;
using System.Threading.Tasks;

namespace KIS.NET.Core.Execution
{
    /// <summary>
    /// An interface designed to execute functions in a separate <see cref="T:System.Threading.Tasks.Task" />
    /// with the ability to cancel it by providing a cancellation token.
    /// </summary>
    /// <typeparam name="TCancellationToken">Type of the cancellation token.</typeparam>
    public interface ICancellableExecutor<in TCancellationToken>
    {
        /// <summary>
        /// Executes the given action with the ability to cancel it using
        /// the given cancellation token.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken">Cancellation token to apply if required.</param>
        /// <returns>Executing task.</returns>
        Task Execute(Action<CancellationToken> action, TCancellationToken cancellationToken);

        /// <summary>
        /// Executes the given function with the ability to cancel it using
        /// the given cancellation token.
        /// </summary>
        /// <typeparam name="TResult">Type of the function's return value.</typeparam>
        /// <param name="function"></param>
        /// <param name="cancellationToken">Cancellation token to apply if required.</param>
        /// <returns>Created task promising to return the function's return value
        /// once it's finished.</returns>
        Task<TResult> Execute<TResult>(
            Func<CancellationToken, TResult> function,
            TCancellationToken cancellationToken);
    }
}