namespace Artech.Pipelines
{
    /// <summary>
    ///  Represents the provider to register and get named pipeline.
    /// </summary>
    public interface IPipelineProvider
    {
        /// <summary>Registers the built pipeline.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <param name="name">The registration name of pipeline.</param>
        /// <param name="setup">The delegate to built the pipeline to register.</param>
        /// <returns>The current <see cref="IPipelineProvider"/>.</returns>
        IPipelineProvider AddPipeline<TContext>(string name, Action<IPipelineBuilder<TContext>> setup);

        /// <summary>Tries to get the restered pipeline based on specified name.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <param name="name">The registration name of pipeline.</param>
        /// <param name="pipeline">The <see cref="Func{TContext, ValueTask}"/> representing the pipeline to get.</param>
        /// <returns>A <see cref="Boolean"/> value indicating whether the specified pipeline exists.</returns>
        bool TryGetPipeline<TContext>(string name, out Func<TContext, ValueTask>? pipeline);

        /// <summary>Exports all pipelines.</summary>
        /// <returns>The dictionary containing all named pipeline based <see cref="PipeDescriptorInfo"/>.</returns>
        IDictionary<string, PipeDescriptorInfo> ExportAllPipelines();
    }
}
