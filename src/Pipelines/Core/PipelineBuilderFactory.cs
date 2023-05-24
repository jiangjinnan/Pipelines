namespace Artech.Pipelines
{
    internal sealed class PipelineBuilderFactory : IPipelineBuilderFactory
    {
        private readonly IServiceProvider _applicationServices;
        public PipelineBuilderFactory(IServiceProvider applicationServices) => _applicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
        public IPipelineBuilder<TContext> Create<TContext>()=> new PipelineBuilder<TContext>(_applicationServices);
    }
}
