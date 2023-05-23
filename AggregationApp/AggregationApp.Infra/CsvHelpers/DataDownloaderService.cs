using AggregationApp.Core;

namespace AggregationApp.Infra.CsvHelpers
{
    public class DataDownloaderService : IDataDownloaderService
    {
        private readonly ICSVReader _csvReader;

        public DataDownloaderService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
        }

        public async Task<List<ElectricityData>> DownloadData(List<string> urls)
        {
            var downloadTasks = urls.Select(url => _csvReader.Get<ElectricityConsumptionCsvModel>(url))
                                    .ToList();

            var results = await Task.WhenAll(downloadTasks);

            var electricityConsumptions = results.SelectMany(x => x)
                                                 .Select(x => new ElectricityData(
                                                     x.Tinkla,
                                                     x.ObtPavadinimas,
                                                     x.PPlus,
                                                     x.PMinus))
                                                 .ToList();
            return electricityConsumptions;
        }
    }
}