using Flight.Extensions.Primitives;

namespace Artech.Pipelines
{
    /// <summary>
    /// Defines extension methods against <see cref="IPipelineProvider"/>.
    /// </summary>
    public static class PipelineProviderExtensions
    {
        /// <summary>Registers a default built pipeline.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <param name="pipelineProvider">The <see cref="IPipelineProvider"/> used for pipeline provision.</param>
        /// <param name="setup">The delegate to built the pipeline to register.</param>
        /// <returns>The current <see cref="IPipelineProvider"/>.</returns>
        public static IPipelineProvider AddPipeline<TContext>(this IPipelineProvider pipelineProvider, Action<IPipelineBuilder<TContext>> setup)
        {
            Guard.ArgumentNotNull(pipelineProvider, nameof(pipelineProvider));
            return pipelineProvider.AddPipeline(PipelineDefaults.DefaultPipelineName, setup);
        }

        /// <summary>
        /// The the pipeline based on specified name.
        /// </summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <param name="pipelineProvider">The <see cref="IPipelineProvider"/> used for pipeline provision.</param>
        /// <param name="name">The pipeline's name.</param>
        /// <returns>The <see cref="IPipeline{TContext}"/> representing the pipeline.</returns>
        public static IPipeline<TContext> GetPipeline<TContext>(this IPipelineProvider pipelineProvider, string name)
        {
            Guard.ArgumentNotNull(pipelineProvider, nameof(pipelineProvider));
            if (pipelineProvider.TryGetPipeline<TContext>(name, out var pipeline))
            { 
                return new Pipeline<TContext>(pipeline!);
            }
            throw new InvalidOperationException($"The pipeline '{name}' is not registered.");
        }

        /// <summary>
        /// The the pipeline based on specified name.
        /// </summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <param name="pipelineProvider">The <see cref="IPipelineProvider"/> used for pipeline provision.</param>
        /// <returns>The <see cref="IPipeline{TContext}"/> representing the pipeline.</returns>
        public static IPipeline<TContext> GetPipeline<TContext>(this IPipelineProvider pipelineProvider) => pipelineProvider.GetPipeline<TContext>(PipelineDefaults.DefaultPipelineName);
    }
}
