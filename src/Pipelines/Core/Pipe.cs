namespace Artech.Pipelines
{
    /// <summary>
    /// Represents a single pipe of which the pipeline is composed.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Pipe<TContext>
    {
        /// <summary>
        /// The functional description.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Exports the pipe's descriptive information.
        /// </summary>
        /// <param name="next">The <see cref="PipeDescriptorInfo"/> describing the next pipe in the pipeline.</param>
        /// <returns>The <see cref="PipeDescriptorInfo"/>describing the pipe.</returns>
        public virtual PipeDescriptorInfo Export(PipeDescriptorInfo next) => new(Description, next);

        /// <summary>
        /// Invoke the functional operation.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="next">The delegate used to invoke the next pipe.</param>
        /// <returns>The <see cref="ValueTask"/> to invoke the functional operation.</returns>
        public abstract ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next);
    }
}