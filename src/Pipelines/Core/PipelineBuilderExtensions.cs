using Microsoft.Extensions.DependencyInjection;

namespace Artech.Pipelines
{
    /// <summary>
    ///   Defines extension methods to register pipe to specified <see cref="IPipelineBuilder{TContext}"/>
    /// </summary>
    public static class PipelineBuilderExtensions
    {
        /// <summary>Register specified type of pipe.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <typeparam name="TPipe">The type of the pipe to register.</typeparam>
        /// <param name="pipleineBuilder">The <see cref="IPipelineBuilder{TContext}"/> to builld pipeline with registered pipes.</param>
        /// <param name="arguments">The extra arguments passed to pipe type's constructor.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/></returns>
        public static IPipelineBuilder<TContext> Use<TContext, TPipe>(this IPipelineBuilder<TContext> pipleineBuilder, params object[] arguments) where TPipe : Pipe<TContext>
        {
            if (pipleineBuilder == null)
            {
                throw new ArgumentNullException(nameof(pipleineBuilder));
            }
            var pipe = ActivatorUtilities.CreateInstance<TPipe>(pipleineBuilder.ApplicationServices, arguments);
            return pipleineBuilder.Use(pipe);
        }
    }
}