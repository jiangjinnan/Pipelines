namespace Artech.Pipelines
{
    /// <summary>
    ///   The pipe executing functional operation by invoking specified delegate.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public sealed class DelegatePipe<TContext> : Pipe<TContext>
    {
        private readonly Func<TContext, Func<TContext, ValueTask>, ValueTask> _handler;

        /// <summary>Gets the description.</summary>
        /// <value>The description.</value>
        public override string Description { get; }

        /// <summary>Initializes a new instance of the <see cref="DelegatePipe{TContext}" /> class.</summary>
        /// <param name="handler">The handler.</param>
        /// <param name="description">The description.</param>
        public DelegatePipe(Func<TContext, Func<TContext, ValueTask>, ValueTask> handler, string description)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>Initializes a new instance of the <see cref="DelegatePipe{TContext}" /> class.</summary>
        /// <param name="handler">The handler.</param>
        /// <param name="description">The description.</param>
        public DelegatePipe(Func<TContext, Func<TContext, ValueTask>, Task> handler, string description)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _handler = async (context, next) =>
            {
                await handler(context, next);
                await next(context);
            };
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatePipe{TContext}"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="description">The description.</param>
        public DelegatePipe(Func<TContext, ValueTask> handler, string description)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _handler = async (context, next) =>
            {
                await handler(context);
                await next(context);
            };
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatePipe{TContext}"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="description">The description.</param>
        public DelegatePipe(Func<TContext, Task> handler, string description)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _handler = async (context, next) =>
            {
                await handler(context);
                await next(context);
            };
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>Initializes a new instance of the <see cref="DelegatePipe{TContext}" /> class.</summary>
        /// <param name="handler">The handler.</param>
        /// <param name="description">The description.</param>
        public DelegatePipe(Action<TContext> handler, string description)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _handler = (context, next) =>
            {
                handler(context);
                return next(context);
            };
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Invoke the functional operation.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="next">The delegate used to invoke the next pipe.</param>
        /// <returns>
        /// The <see cref="ValueTask" /> to invoke the functional operation.
        /// </returns>
        public override ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next) => _handler(context, next);
    }
}
