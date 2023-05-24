using Flight.Extensions.Primitives;

namespace Artech.Pipelines
{
    internal sealed class Pipeline<TContext> : IPipeline<TContext>
    {
        private readonly Func<TContext, ValueTask> _pipeline;
        public Pipeline(IPipelineProvider pipelineProvider)
        {
            Guard.ArgumentNotNull(pipelineProvider, nameof(pipelineProvider));
            if (!pipelineProvider.TryGetPipeline<TContext>(PipelineDefaults.DefaultPipelineName, out var pipeline))
            {
                throw new InvalidOperationException($"The default pipeline specific to '{typeof(TContext)}' is not registered.");
            }
            _pipeline = pipeline!;
        }

        internal Pipeline(Func<TContext, ValueTask> pipeline) => _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));

        public ValueTask ProcessAsync(TContext context) => _pipeline(context);
    }
}