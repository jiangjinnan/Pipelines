using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Artech.Pipelines
{
    internal class PipelineStartupFilter : IStartupFilter
    {
        private readonly Action<IPipelineProvider> _buildPipelines;

        public PipelineStartupFilter(Action<IPipelineProvider> buildPipelines)
        {
            _buildPipelines = buildPipelines ?? throw new ArgumentNullException(nameof(buildPipelines));
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder => {
                var pipelineProvider = builder.ApplicationServices.GetRequiredService<IPipelineProvider>();
                _buildPipelines(pipelineProvider);
                builder.UseRouting();
                builder.UseEndpoints(endpoints => endpoints.MapPipelines());
                next(builder);
            };
        }
    }
}