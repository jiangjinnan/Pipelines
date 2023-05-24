using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Artech.Pipelines
{
    /// <summary>
    /// Defines extension methods to register pipeline based services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>Registers pipeline based services.</summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="buildPipelines">The <see cref="Action{IPipelineProvider}"/> to build pipelines.</param>
        /// <returns>The current <see cref="IServiceCollection"/>.</returns>        
        public static IServiceCollection AddPipelines(this IServiceCollection services, Action<IPipelineProvider> buildPipelines)
        {
            if (services == null)
            { 
                throw new ArgumentNullException(nameof(services));
            }
            if (buildPipelines == null)
            {
                throw new ArgumentNullException(nameof(buildPipelines));
            }
            services.TryAdd(ServiceDescriptor.Singleton<IPipelineBuilderFactory, PipelineBuilderFactory>());
            services.TryAdd(ServiceDescriptor.Singleton<IPipelineProvider, PipelineProvider>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IStartupFilter, PipelineStartupFilter>(Create));
            services.TryAdd(ServiceDescriptor.Singleton<PipelineMarkerService, PipelineMarkerService>());
            services.TryAddSingleton(typeof(IPipeline<>), typeof(Pipeline<>));
            return services;
            PipelineStartupFilter Create(IServiceProvider serviceProvider) => ActivatorUtilities.CreateInstance<PipelineStartupFilter>(serviceProvider, buildPipelines);
        }
    }
}
