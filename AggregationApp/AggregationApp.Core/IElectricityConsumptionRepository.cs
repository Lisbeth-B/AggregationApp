namespace AggregationApp.Core
{
    public interface IElectricityConsumptionRepository
    {
        Task AddAsync(List<ElectricityData> data);
        Task<List<ElectricityData>> GetAllDataAsync();
    }
}