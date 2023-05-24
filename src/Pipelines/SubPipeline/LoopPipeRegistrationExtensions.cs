namespace Artech.Pipelines
{
    /// <summary>
    ///   Defines extension methods to register <see cref="LoopPipe{TContext, TSubContext, TItem}"/>.
    /// </summary>
    public static class LoopPipeRegistrationExtensions
    {
        /// <summary>Registers <see cref="LoopPipe{TContext, TSubContext, TItem}"/>.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <typeparam name="TSubContext">The type of the sub pipeline execution context.</typeparam>
        /// <typeparam name="TItem">The type of the element of collection.</typeparam>
        /// <param name="pipelineBuilder">The <see cref="IPipelineBuilder{TContext}"/> used to build pipeline with registered pipes.</param>
        /// <param name="description">The descriptive information.</param>
        /// <param name="collectionAccessor">The delegate to get collection to loop.</param>
        /// <param name="filter">The <see cref="Func{TContext, TItem, Boolean}"/> used to filter the iterated item.</param>
        /// <param name="subPipelineSetup">The sub pipeline setup.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/></returns>
        public static IPipelineBuilder<TContext> ForEach<TContext, TSubContext, TItem>(
            this IPipelineBuilder<TContext> pipelineBuilder,
            string description,
            Func<TContext, IEnumerable<TItem>> collectionAccessor,
            Func<TContext, TItem, bool> filter,
            Action<IPipelineBuilder<TSubContext>> subPipelineSetup)
            where TContext : ContextBase
            where TSubContext : SubContextBase<TContext, TItem>, new()
        {
            var subBuilder = pipelineBuilder.CreateNew<TSubContext>();
            (subPipelineSetup ?? throw new ArgumentNullException(nameof(subPipelineSetup))).Invoke(subBuilder);
            var subPipeline = subBuilder.Build(out var subPipelineDescriptor);
            var pipe = new LoopPipe<TContext, TSubContext, TItem>(collectionAccessor, filter, subPipeline, subPipelineDescriptor, description);
            return pipelineBuilder.Use(pipe);
        }

        /// <summary>Registers <see cref="LoopPipe{TContext, TSubContext, TItem}"/>.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <typeparam name="TSubContext">The type of the sub pipeline execution context.</typeparam>
        /// <typeparam name="TItem">The type of the element of collection.</typeparam>
        /// <param name="pipelineBuilder">The <see cref="IPipelineBuilder{TContext}"/> used to build pipeline with registered pipes.</param>
        /// <param name="description">The descriptive information.</param>
        /// <param name="collectionAccessor">The delegate to get collection to loop.</param>
        /// <param name="subPipelineSetup">The sub pipeline setup.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/></returns>
        public static IPipelineBuilder<TContext> ForEach<TContext, TSubContext, TItem>(
            this IPipelineBuilder<TContext> pipelineBuilder,
            string description,
            Func<TContext, IEnumerable<TItem>> collectionAccessor,
            Action<IPipelineBuilder<TSubContext>> subPipelineSetup)
            where TContext : ContextBase
            where TSubContext : SubContextBase<TContext, TItem>, new()
        {
            var subBuilder = pipelineBuilder.CreateNew<TSubContext>();
            (subPipelineSetup ?? throw new ArgumentNullException(nameof(subPipelineSetup))).Invoke(subBuilder);
            var subPipeline = subBuilder.Build(out var subPipelineDescriptor);
            var pipe = new LoopPipe<TContext, TSubContext, TItem>(collectionAccessor, (_,_) => true, subPipeline, subPipelineDescriptor, description);
            return pipelineBuilder.Use(pipe);
        }
    }
}
