using Artech.Pipelines;

namespace App
{
    public sealed class StatePopulationContext : ContextBase
    {
        public StatePopulationData PopulationData { get; }
        public StatePopulationContext(StatePopulationData populationData) => PopulationData = populationData;
    }

    public sealed class ProvincePopulationContext : SubContextBase<StatePopulationContext, KeyValuePair<string, ProvincePopulationData>>
    {
        public string Province { get; private set; } = default!;
        public IDictionary<string, PopulationData> Cities { get; private set; } = default!;
        public override void Initialize(StatePopulationContext parent, KeyValuePair<string, ProvincePopulationData> item)
        {
            Province = item.Key;
            Cities = item.Value.Cities;
            base.Initialize(parent, item);
        }
    }

    public sealed class CityPopulationContext : SubContextBase<ProvincePopulationContext, KeyValuePair<string, PopulationData>>
    {
        public string City { get; private set; } = default!;
        public PopulationData PopulationData { get; private set; } = default!;
        public override void Initialize(ProvincePopulationContext parent, KeyValuePair<string, PopulationData> item)
        {
            City = item.Key;
            PopulationData = item.Value;
            base.Initialize(parent, item);
        }
    }
}
