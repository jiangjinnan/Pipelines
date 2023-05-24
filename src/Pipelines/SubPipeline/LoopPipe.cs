namespace Artech.Pipelines
{
    /// <summary>
    /// A pipe which repeatedly execute its sub pipeline against each item of a collection from current context.
    /// </summary>
    /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
    /// <typeparam name="TSubContext">The type of the sub pipeline execution context.</typeparam>
    /// <typeparam name="TItem">The type of the element of collection.</typeparam>
    public sealed class LoopPipe<TContext, TSubContext, TItem> : Pipe<TContext>
        where TContext : ContextBase
        where TSubContext : SubContextBase<TContext, TItem>, new()
    {
        #region Fields
        private readonly Func<TSubContext, ValueTask> _subPipeline;
        private readonly Func<TContext, IEnumerable<TItem>> _collectionAccessor;
        private readonly Func<TContext, TItem, bool> _itemFilter;
        private readonly PipeDescriptorInfo _subPipelineDescriptor;
        private readonly string? _description;
        #endregion

        #region Properties
        /// <summary>The functional description.</summary>
        public override string Description => _description ?? $"For each {typeof(TItem)}";

        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="LoopPipe{TContext, TSubContext, TItem}" /> class.</summary>
        /// <param name="collectionAccessor">The delegate used to get the collection to loop.</param>
        /// <param name="filter">The <see cref="Func{TContext, TItem, Boolean}"/> used to filter the iterated item.</param>
        /// <param name="subPipeline">The <see cref="Func{TSubContext, ValueTask}"/>representing the sub pipeline.</param>
        /// <param name="subPipelineDescriptor">The <see cref="PipeDescriptorInfo"/> describing the sub pipeline descriptor.</param>
        /// <param name="description">The descriptive information.</param>
        public LoopPipe(
            Func<TContext, IEnumerable<TItem>> collectionAccessor,
            Func<TContext, TItem, bool> filter,
            Func<TSubContext, ValueTask> subPipeline,
            PipeDescriptorInfo subPipelineDescriptor,
            string? description = null)
        {
            _collectionAccessor = collectionAccessor ?? throw new ArgumentNullException(nameof(collectionAccessor));
            _subPipeline = subPipeline ?? throw new ArgumentNullException(nameof(subPipeline));
            _itemFilter = filter ?? throw new ArgumentNullException(nameof(filter));
            _subPipelineDescriptor = subPipelineDescriptor ?? throw new ArgumentNullException(nameof(subPipelineDescriptor));
            _description = description;
        }
        #endregion

        #region Public methods
        /// <summary>Exports the pipe's descriptive information.</summary>
        /// <param name="next">The <see cref="T:Artech.Pipelines.PipeDescriptorInfo" /> describing the next pipe in the pipeline.</param>
        /// <returns>The <see cref="T:Artech.Pipelines.PipeDescriptorInfo" />describing the pipe.</returns>
        public override PipeDescriptorInfo Export(PipeDescriptorInfo next)
        {
            var pipeInfo = base.Export(next);
            pipeInfo.SubPipeline = _subPipelineDescriptor;
            return pipeInfo;
        }

        /// <summary>Invoke the functional operation.</summary>
        /// <param name="context">The execution context.</param>
        /// <param name="next">The delegate used to invoke the next pipe.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.ValueTask">ValueTask</see> to invoke the functional operation.</returns>
        public override async ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next)
        {
            var collection = _collectionAccessor(context);
            foreach (var item in collection)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                if (_itemFilter(context, item))
                {
                    var subContext = new TSubContext();
                    subContext.Initialize(context, item);
                    await _subPipeline(subContext);
                }
            }
            await next(context);
        }
        #endregion
    }
}
