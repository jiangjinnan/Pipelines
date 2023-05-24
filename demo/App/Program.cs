using App;
using Artech.Pipelines;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPipelines(BuildPipelines);
var app = builder.Build();
app.MapGet("/test", HandleRequest);
app.Run();

static void BuildPipelines(IPipelineProvider pipelineProvider)
{
    pipelineProvider.AddPipeline<StatePopulationContext>(name: "PopulationProcessor", setup: builder => builder
      .UseStatePipe<FooStatePipe>()
      .UseStatePipe<BarStatePipe>()
      .ForEachProvince(provinceBuilder => provinceBuilder
          .UseProvincePipe<FooProvincePipe>()
          .UseProvincePipe<BarProvincePipe>()
          .ForEachCity(cityBuilder => cityBuilder
              .UseCityPipe<FooCityPipe>()
              .UseCityPipe<BarCityPipe>()
              .UseCityPipe<BazCityPipe>())
          .UseProvincePipe<BazProvincePipe>())
      .UseStatePipe<BazStatePipe>());
}

static async Task<IResult> HandleRequest(HttpContext httpContext, IPipelineProvider provider, HttpResponse response)
{
    Console.WriteLine("Execute PopulationProcessor pipeline");
    var data = new StatePopulationData
    {
        Provinces = new Dictionary<string, ProvincePopulationData>()
    };
    data.Provinces.Add("Jiangsu", new ProvincePopulationData
    {
        Cities = new Dictionary<string, PopulationData>
        {
            {"Suzhou", new PopulationData() },
            {"Wuxi", new PopulationData() },
            {"Changezhou", new PopulationData() },
        }
    });

    data.Provinces.Add("Shandong", new ProvincePopulationData
    {
        Cities = new Dictionary<string, PopulationData>
        {
            {"Qingdao", new PopulationData() },
            {"Jinan", new PopulationData() },
            {"Yantai", new PopulationData() },
        }
    });

    data.Provinces.Add("Zhejiang", new ProvincePopulationData
    {
        Cities = new Dictionary<string, PopulationData>
        {
            {"Hangzhou", new PopulationData() },
            {"Ningbo", new PopulationData() },
            {"Wenzhou", new PopulationData() },
        }
    });

    var context = new StatePopulationContext(data);
    
    var pipeline = provider.GetPipeline<StatePopulationContext>("PopulationProcessor");
    var valueTask = pipeline.ProcessAsync(context);
    if (!valueTask.IsCompleted)
    {
        await valueTask;
    }
    return Results.Ok("OK");
}


