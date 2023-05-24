namespace Artech.Pipelines
{
    /// <summary>
    ///   Represents the factory to create <see cref="IPipelineBuilder{TContext}"/>.
    /// </summary>
    public interface IPipelineBuilderFactory
    {
        /// <summary>Creates the <see cref="IPipelineBuilder{TContext}"/>.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <returns>The created <see cref="IPipelineBuilder{TContext}"/>.</returns>
        IPipelineBuilder<TContext> Create<TContext>();
    }
}
