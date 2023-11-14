using System;
using System.Threading.Tasks;

namespace KIS.NET.Core.Execution
{
    /// <summary>
    /// An interface designed to execute functions in a separate <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Executes the given action in a separate <see cref="T:System.Threading.Tasks.Task" />.
        /// </summary>
        /// <param name="actionToExecute">Action to execute.</param>
        /// <returns>Executing task.</returns>
        Task Execute(Action actionToExecute);

        /// <summary>
        /// Executes the given function in a separate <see cref="T:System.Threading.Tasks.Task" />.
        /// </summary>
        /// <param name="function">Function to execute.</param>
        /// <typeparam name="TResult">Type of the function's return value.</typeparam>
        /// <returns>Executing task promising to return the function's return value
        /// once it's finished.</returns>
        Task<TResult> Execute<TResult>(Func<TResult> function);
    }
}