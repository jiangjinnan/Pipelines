using Artech.Pipelines;

namespace App
{
public static class Extensions
{
    public static IPipelineBuilder<StatePopulationContext> UseStatePipe<TPipe>(this IPipelineBuilder<StatePopulationContext> builder) 
        where TPipe : Pipe<StatePopulationContext> 
        => builder.Use<StatePopulationContext, TPipe>();
    public static IPipelineBuilder<ProvincePopulationContext> UseProvincePipe<TPipe>(this IPipelineBuilder<ProvincePopulationContext> builder) 
        where TPipe : Pipe<ProvincePopulationContext> 
        => builder.Use<ProvincePopulationContext, TPipe>();
    public static IPipelineBuilder<CityPopulationContext> UseCityPipe<TPipe>(this IPipelineBuilder<CityPopulationContext> builder) 
        where TPipe : Pipe<CityPopulationContext> 
        => builder.Use<CityPopulationContext, TPipe>();

    public static IPipelineBuilder<StatePopulationContext> ForEachProvince(this IPipelineBuilder<StatePopulationContext> builder, Action<IPipelineBuilder<ProvincePopulationContext>> setup)
        => builder.ForEach("For each province", it => it.PopulationData.Provinces, (_, _) => true, setup);
    public static IPipelineBuilder<ProvincePopulationContext> ForEachCity(this IPipelineBuilder<ProvincePopulationContext> builder, Action<IPipelineBuilder<CityPopulationContext>> setup)
        => builder.ForEach("For each city", it => it.Cities, (_, _) => true, setup);
}
}
