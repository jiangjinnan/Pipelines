namespace Artech.Pipelines
{
    /// <summary>
    ///   Represents the abortable pipeline based execution context.
    /// </summary>
    public interface IAbortableContext
    {
        /// <summary>
        /// Gets a value indicating whether this pipeline execution is aborted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this pipeline execution is aborted; otherwise, <c>false</c>.
        /// </value>
        bool IsAborted { get; }

        /// <summary>Aborts this pipeline execution.</summary>
        void Abort();
    }
}
