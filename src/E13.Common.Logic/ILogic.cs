using System;

namespace E13.Common.Logic
{
    public interface ILogic<TContext> where TContext : IDisposable
    {
        /// <summary>
        /// Gets the IDisposable Context
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        TContext Context { get; }
        
    }
}
