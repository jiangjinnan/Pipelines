namespace Artech.Pipelines
{
    /// <summary>
    /// Represents a builder to build pipeline with registered pipes.
    /// </summary>
    /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
    public interface IPipelineBuilder<TContext>
    {
        /// <summary>Gets the application services (application wide dependency injection container).</summary>
        /// <value>The application services (application wide dependency injection container).</value>
        IServiceProvider ApplicationServices { get; }

        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        IDictionary<string, object> Properties { get; }

        /// <summary>Add the specified pipe.</summary>
        /// <param name="pipe">The pipe to add.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/></returns>
        IPipelineBuilder<TContext> Use(Pipe<TContext> pipe);

        /// <summary>Builds the pipeline with added pipes.</summary>
        /// <param name="exportedPipeline">The exported pipeline descriptive information.</param>
        /// <returns>The <see cref="Func{TContext,ValueTask}"/> representing the built pipeline.</returns>
        Func<TContext, ValueTask> Build(out PipeDescriptorInfo exportedPipeline);

        /// <summary>Creates the new <see cref="IPipelineBuilder{TPipelineContext}"/>.</summary>
        /// <typeparam name="TPipelineContext">The type of the pipeline context.</typeparam>
        /// <returns>The created <see cref="IPipelineBuilder{TPipelineContext}"/>.</returns>
        IPipelineBuilder<TPipelineContext> CreateNew<TPipelineContext>();
    }
}
