namespace Artech.Pipelines
{
    /// <summary>
    /// A base class for synchronous pipe classes.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="Artech.Pipelines.Pipe&lt;TContext&gt;" />
    public abstract class PipeBase<TContext> : Pipe<TContext>
    {
        /// <summary>
        /// Invoke the functional operation.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="next">The delegate used to invoke the next pipe.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.ValueTask" /> to invoke the functional operation.
        /// </returns>
        public override ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next)
        {
            Invoke(context);
            return next(context);
        }
        /// <summary>
        /// Invoke the functional operation synchronously.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract void Invoke(TContext context);
    }
}
