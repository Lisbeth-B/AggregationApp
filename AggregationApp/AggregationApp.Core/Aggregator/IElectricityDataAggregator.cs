namespace AggregationApp.Core
{
    public interface IElectricityDataAggregator
    {
        List<ElectricityData> AggregateData(List<ElectricityData> data, string obtPavadinimas);
    }
}