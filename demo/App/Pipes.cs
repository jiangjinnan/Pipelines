using Artech.Pipelines;

namespace App
{
    public sealed class FooStatePipe : PipeBase<StatePopulationContext>
    {
        public override string Description => "State Population Processor Foo";
        protected override void Invoke(StatePopulationContext context)
            => Console.WriteLine("Foo: Process state population");
    }
    public sealed class BarStatePipe : PipeBase<StatePopulationContext>
    {
        public override string Description => "State Population Processor Bar";
        protected override void Invoke(StatePopulationContext context)
            => Console.WriteLine("Bar: Process state population");
    }
    public sealed class BazStatePipe : PipeBase<StatePopulationContext>
    {
        public override string Description => "State Population Processor Baz";
        protected override void Invoke(StatePopulationContext context)
            => Console.WriteLine("Baz: Process state population");
    }

    public sealed class FooProvincePipe : PipeBase<ProvincePopulationContext>
    {
        public override string Description => "Province Population Processor Foo";
        protected override void Invoke(ProvincePopulationContext context)
            => Console.WriteLine($"\tFoo: Process population of the province {context.Province}");
    }

    public sealed class BarProvincePipe : PipeBase<ProvincePopulationContext>
    {
        public override string Description => "Province Population Processor Bar";
        protected override void Invoke(ProvincePopulationContext context)
            => Console.WriteLine($"\tBar: Process population of the province {context.Province}");

    }
    public sealed class BazProvincePipe : PipeBase<ProvincePopulationContext>
    {
        public override string Description => "Province Population Processor Baz";
        protected override void Invoke(ProvincePopulationContext context)
            => Console.WriteLine($"\tBaz: Process population of the province {context.Province}");
    }

    public sealed class FooCityPipe : PipeBase<CityPopulationContext>
    {
        public override string Description => "City Population Processor Foo";
        protected override void Invoke(CityPopulationContext context)
            => Console.WriteLine($"\t\tFoo: Process population of the city {context.City}");
    }

    public sealed class BarCityPipe : PipeBase<CityPopulationContext>
    {
        public override string Description => "City Population Processor Bar";
        protected override void Invoke(CityPopulationContext context)
            => Console.WriteLine($"\t\tBar: Process population of the city {context.City}");

    }

    public sealed class BazCityPipe : PipeBase<CityPopulationContext>
    {
        public override string Description => "City Population Processor Baz";
        protected override void Invoke(CityPopulationContext context)
            => Console.WriteLine($"\t\tBaz: Process population of the city {context.City}");
    }
}
