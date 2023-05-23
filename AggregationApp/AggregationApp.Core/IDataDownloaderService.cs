namespace AggregationApp.Core
{
    public interface IDataDownloaderService
    {
        Task<List<ElectricityData>> DownloadData(List<string> urls);
    }
}