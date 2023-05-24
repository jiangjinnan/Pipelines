namespace Artech.Pipelines
{
    /// <summary>
    /// Represents a pipeline based on specified context.
    /// </summary>
    /// <typeparam name="TContext">The type of pipeline execution context.</typeparam>
    public interface IPipeline<TContext>
    {
        /// <summary>
        /// Execute the pipeline against specified execution context.
        /// </summary>
        /// <param name="context">Pipeline execution context.</param>
        /// <returns>The <see cref="ValueTask"/> to execute the pipeline against specified execution context.</returns>
        ValueTask ProcessAsync(TContext context);
    }
}
