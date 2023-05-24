using Flight.Extensions.Primitives;

namespace Artech.Pipelines
{
    /// <summary>
    ///   <br />
    /// </summary>
    public static class DelegatePipeRegistrationExtensions
    {
        /// <summary>Registers a <see cref="DelegatePipe{TContext}"/> based on specified handler.</summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The <see cref="IPipelineBuilder{TContext}"/>.</param>
        /// <param name="handler">The handler of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <param name="description">The description of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/>.</returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, Func<TContext, ValueTask>, ValueTask> handler, string description)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            Guard.ArgumentNotNull(handler, nameof(handler));
            Guard.ArgumentNotNullOrWhiteSpace(description, nameof(description));
            return builder.Use(new DelegatePipe<TContext>(handler, description));
        }

        /// <summary>Registers a <see cref="DelegatePipe{TContext}"/> based on specified handler.</summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The <see cref="IPipelineBuilder{TContext}"/>.</param>
        /// <param name="handler">The handler of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <param name="description">The description of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/>.</returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, ValueTask> handler, string description)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            Guard.ArgumentNotNull(handler, nameof(handler));
            Guard.ArgumentNotNullOrWhiteSpace(description, nameof(description));
            return builder.Use(new DelegatePipe<TContext>(handler, description));
        }

        /// <summary>Registers a <see cref="DelegatePipe{TContext}"/> based on specified handler.</summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The <see cref="IPipelineBuilder{TContext}"/>.</param>
        /// <param name="handler">The handler of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <param name="description">The description of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/>.</returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext> handler, string description)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            Guard.ArgumentNotNull(handler, nameof(handler));
            Guard.ArgumentNotNullOrWhiteSpace(description, nameof(description));
            return builder.Use(new DelegatePipe<TContext>(handler, description));
        }

        /// <summary>Registers a <see cref="DelegatePipe{TContext}"/> based on specified handler.</summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The <see cref="IPipelineBuilder{TContext}"/>.</param>
        /// <param name="handler">The handler of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <param name="description">The description of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/>.</returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, Func<TContext, ValueTask>, Task> handler, string description)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            Guard.ArgumentNotNull(handler, nameof(handler));
            Guard.ArgumentNotNullOrWhiteSpace(description, nameof(description));
            return builder.Use(new DelegatePipe<TContext>(handler, description));
        }

        /// <summary>Registers a <see cref="DelegatePipe{TContext}"/> based on specified handler.</summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The <see cref="IPipelineBuilder{TContext}"/>.</param>
        /// <param name="handler">The handler of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <param name="description">The description of the <see cref="DelegatePipe{TContext}"/> to register.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/>.</returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, Task> handler, string description)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            Guard.ArgumentNotNull(handler, nameof(handler));
            Guard.ArgumentNotNullOrWhiteSpace(description, nameof(description));
            return builder.Use(new DelegatePipe<TContext>(handler, description));
        }
    }
}
