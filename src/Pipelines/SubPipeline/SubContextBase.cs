namespace Artech.Pipelines
{
    /// <summary>
    ///   The base type of sub pipeline execution context.
    /// </summary>
    /// <typeparam name="TParentContext">The type of the parent pipeline execution context.</typeparam>
    /// <typeparam name="TItem">The type of the element of collection.</typeparam>
    public abstract class SubContextBase<TParentContext, TItem> : ContextBase where TParentContext : ContextBase
    {
        /// <summary>Gets the parent pipeline execution context.</summary>
        /// <value>The parent pipeline execution context.</value>
        public TParentContext Parent { get; private set; } = default!;

        /// <summary>Gets the item of the collection to loop.</summary>
        /// <value>The item of the collection to loop.</value>
        public TItem Item { get; private set; } = default!;

        /// <summary>Gets the cancellation token.</summary>
        /// <value>The cancellation token.</value>
        public override CancellationToken CancellationToken => Parent?.CancellationToken ?? CancellationToken.None;

        /// <summary>Initializes the current pipeline execution context.</summary>
        /// <param name="parent">The current pipeline execution context.</param>
        /// <param name="item">The item of collection to loop.</param>
        public virtual void Initialize(TParentContext parent, TItem item)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Item = item;
        }
    }
}
