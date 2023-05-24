namespace Artech.Pipelines
{
    /// <summary>
    ///  The base type of pipeline execution context.
    /// </summary>
    public abstract class ContextBase : IAbortableContext, ICancellableContext
    {
        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>Gets a value indicating whether this pipeline execution is aborted.</summary>
        /// <value>
        ///   <c>true</c> if this pipeline execution is aborted; otherwise, <c>false</c>.</value>
        public bool IsAborted { get; private set; }

        /// <summary>Gets the cancellation token.</summary>
        /// <value>The cancellation token.</value>
        public virtual CancellationToken CancellationToken => CancellationToken.None;

        /// <summary>Aborts this pipeline execution.</summary>
        public void Abort() => IsAborted = true;
    }
}
