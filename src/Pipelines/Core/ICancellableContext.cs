namespace Artech.Pipelines
{
    /// <summary>
    /// Represents the cancellable pipeline execution context.
    /// </summary>
    public interface ICancellableContext
    {
        /// <summary>Gets the cancellation token.</summary>
        /// <value>The cancellation token.</value>
        CancellationToken CancellationToken { get; }
    }
}
