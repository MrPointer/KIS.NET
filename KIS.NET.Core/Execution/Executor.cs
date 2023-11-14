using System;
using System.Threading.Tasks;

namespace KIS.NET.Core.Execution
{
    /// <summary>
    /// A class used to execute actions or functions in a separate <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    public class Executor : IExecutor
    {
        /// <inheritdoc />
        public Task Execute(Action actionToExecute)
        {
            return Task.Run(actionToExecute);
        }

        /// <inheritdoc />
        public Task<TResult> Execute<TResult>(Func<TResult> function)
        {
            return Task.Run(function);
        }
    }
}