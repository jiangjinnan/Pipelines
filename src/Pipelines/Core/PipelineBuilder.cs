using Microsoft.Extensions.DependencyInjection;

namespace Artech.Pipelines
{
    internal class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        #region Fields
        private readonly List<Pipe<TContext>> _pipes = new();
        private Dictionary<string, object> _properties = new();
        #endregion

        #region Properties
        public IServiceProvider ApplicationServices { get; }
        public IDictionary<string, object> Properties => _properties ??= new();
        #endregion

        #region Constructors
        public PipelineBuilder(IServiceProvider? applicationServices = null) => ApplicationServices = applicationServices ?? new ServiceCollection().BuildServiceProvider();
        #endregion

        #region Public methods
        public IPipelineBuilder<TContext> Use(Pipe<TContext> pipe)
        {
            _pipes.Add(pipe ?? throw new ArgumentNullException(nameof(pipe)));
            return this;
        }

        public IPipelineBuilder<TPipelineContext> CreateNew<TPipelineContext>() => new PipelineBuilder<TPipelineContext>(ApplicationServices) { _properties = _properties };

        public Func<TContext, ValueTask> Build(out PipeDescriptorInfo exportedPipeline)
        {
            PipeDescriptorInfo nextDescriptor = PipeDescriptorInfo.Terminal;
            for (int index = _pipes.Count - 1; index > -1; index--)
            {
                var pipe = _pipes[index];
                nextDescriptor = pipe.Export(nextDescriptor);
            }
            exportedPipeline = nextDescriptor;
            return PipelineBuilder<TContext>.Build(_pipes);
        }
        #endregion

        #region Private methods
        private static Func<TContext, ValueTask> Build(IList<Pipe<TContext>> pipes)
        {
            Func<TContext, ValueTask> nextPipeline = _ => ValueTask.CompletedTask;
            for (int index = pipes.Count - 1; index > -1; index--)
            {
                var pipe = pipes[index];
                var convertedPipe = Convert(pipe);
                nextPipeline = convertedPipe(nextPipeline);
            }
            return nextPipeline;

            static Func<Func<TContext, ValueTask>, Func<TContext, ValueTask>> Convert(Pipe<TContext> pipe)
            {
                return next => context => InvokeAsync(pipe, context, next);
            }

            static ValueTask InvokeAsync(Pipe<TContext> pipe, TContext context, Func<TContext, ValueTask> next)
            {
                if (context is ICancellableContext cancellableContext)
                {
                    cancellableContext.CancellationToken.ThrowIfCancellationRequested();
                }
                if (context is IAbortableContext abortableContext && abortableContext.IsAborted)
                {
                    return ValueTask.CompletedTask;
                }
                return pipe.InvokeAsync(context, next);
            }
        }
        #endregion
    }
}