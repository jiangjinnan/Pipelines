using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Artech.Pipelines
{
    /// <summary>
    ///   Defines extension methods to register routing endpoints to export registered pipelines.
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>Registers routing endpoints to export registered pipelines.</summary>
        /// <param name="endpointRouteBuilder">The <see cref="IEndpointRouteBuilder"/>.</param>
        /// <returns>The current <see cref="IEndpointRouteBuilder"/>.</returns>
        public static IEndpointRouteBuilder MapPipelines(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var markerService = endpointRouteBuilder.ServiceProvider.GetService<PipelineMarkerService>() ?? throw new InvalidOperationException("Pipeline based services are not registered.");
            endpointRouteBuilder.MapGet("pipelines", RenderAllPipelines);
            endpointRouteBuilder.MapGet("pipelines/{pipelineName}", RenderPipeline);
            return endpointRouteBuilder;
        }

        private static IResult RenderAllPipelines(IPipelineProvider pipelineProvider)
        {
            var pipelines = pipelineProvider.ExportAllPipelines();
            var list = pipelines.Keys.Select(it => $"<li><a href='pipelines/{it}'>{it}</li>");
            var html = @$"
<html>
     <head><title>Pipelines</title></head>
     <body>
        <h4>The following pipelines are registered:</h4>
        <ul>
            {string.Join(' ', list)}
        </ul>
     </body>
<html>";
            return Results.Text(html, "text/html");
        }

        private static IResult RenderPipeline(string pipelineName, IPipelineProvider pipelineProvider)
        {
            var pipelines = pipelineProvider.ExportAllPipelines();
            if (!pipelines.TryGetValue(pipelineName, out var descriptor))
            {
                return Results.NotFound();
            }
            return Results.Text(descriptor.ToString());
        }
    }
}
