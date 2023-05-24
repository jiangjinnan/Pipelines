namespace App
{
    public class PopulationData
    {
        public object Statistics { get; set; } = default!;
    }

    public class StatePopulationData
    {
        public IDictionary<string, ProvincePopulationData> Provinces { get; set; } = default!;
    }

    public class ProvincePopulationData
    {
        public IDictionary<string, PopulationData> Cities { get; set; } = default!;
    }
}
