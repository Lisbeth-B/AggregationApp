using AggregationApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace AggregationApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityDataController : ControllerBase
    {
        private readonly IDataDownloaderService _dataDownloaderService;
        private readonly IElectricityConsumptionRepository _electricityConsumptionRepository;
        private readonly IElectricityDataAggregator _electricityDataAggregator;

        public ElectricityDataController(IDataDownloaderService dataDownloaderService,
                                         IElectricityConsumptionRepository electricityConsumptionRepository,
                                         IElectricityDataAggregator electricityDataAggregator)
        {
            _dataDownloaderService = dataDownloaderService;
            _electricityConsumptionRepository = electricityConsumptionRepository;
            _electricityDataAggregator = electricityDataAggregator;
        }

        [HttpPost("Download")]
        public async Task<IActionResult> DownloadCSV()
        {
            var urls = GetUrlsToFetch();
            var data = await _dataDownloaderService.DownloadData(urls);

            var aggregatedData = _electricityDataAggregator.AggregateData(data, "Butas");

            await _electricityConsumptionRepository.AddAsync(aggregatedData);

            return Ok();
        }

        private static List<string> GetUrlsToFetch()
        {
            return new()
        {
            "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv",
            "https://data.gov.lt/dataset/1976/download/10763/2022-03.csv",
            "https://data.gov.lt/dataset/1977/download/10763/2022-04.csv",
            "https://data.gov.lt/dataset/1978/download/10763/2022-05.csv"
        };
        }

        [HttpPost("Get")]
        public async Task<IActionResult> GetAllData()
        {
            var result = await _electricityConsumptionRepository.GetAllDataAsync();
            return Ok(result);
        }
    }

}
