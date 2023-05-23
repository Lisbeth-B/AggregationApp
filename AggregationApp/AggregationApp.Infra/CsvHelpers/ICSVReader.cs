namespace AggregationApp.Core
{
    public interface ICSVReader
    {
        Task<List<T>> Get<T>(string url);
    }
}