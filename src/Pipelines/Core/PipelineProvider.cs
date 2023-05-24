using Microsoft.Extensions.DependencyInjection;

namespace Artech.Pipelines
{
    internal sealed class PipelineProvider : IPipelineProvider
    {
        #region Fields
        private readonly IPipelineBuilderFactory _pipelineBuilderFactory;
        private readonly Dictionary<string, object> _pipelines = new();
        private readonly Dictionary<string, PipeDescriptorInfo> _exportedPipelines = new();
        #endregion

        #region Constructors
        public PipelineProvider(IPipelineBuilderFactory pipelineBuilderFactory)
        => _pipelineBuilderFactory = pipelineBuilderFactory ?? throw new ArgumentNullException(nameof(pipelineBuilderFactory));
        #endregion

        #region Public methods
        public IPipelineProvider AddPipeline<TContext>(string name, Action<IPipelineBuilder<TContext>> setup)
        {
            if (name == null)
            { 
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrWhiteSpace(name))
            { 
                throw new ArgumentException("Specified pipeline name cannot be white space.", nameof(name));
            }
            var builder = _pipelineBuilderFactory.Create<TContext>();
            (setup ?? throw new ArgumentNullException(nameof(setup))).Invoke(builder);
            _pipelines[name] = builder.Build(out var descriptor);
            _exportedPipelines[name] = descriptor;
            return this;
        }

        public IDictionary<string, PipeDescriptorInfo> ExportAllPipelines() => _exportedPipelines;

        public bool TryGetPipeline<TContext>(string name, out Func<TContext, ValueTask>? pipeline)
        {
            if (_pipelines.TryGetValue(name, out var value))
            {
                if (value is Func<TContext, ValueTask> result)
                {
                    pipeline = result;
                    return true;
                }
                throw new ArgumentException("The specified name does not match pipeline execution context type.", nameof(name));
            }
            pipeline = null;
            return false;
        }
        #endregion
    }
}
