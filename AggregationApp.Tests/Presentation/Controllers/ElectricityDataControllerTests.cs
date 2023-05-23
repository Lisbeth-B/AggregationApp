using AggregationApp.Controllers;
using AggregationApp.Core;
using AggregationApp.Core.Filter;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AggregationApp.Tests.Controllers
{
    public class ElectricityDataControllerTests
    {
        [Fact]
        public async Task GetAllData_ReturnsOkResult()
        {
            var dataDownloaderServiceMock = new Mock<IDataDownloaderService>();
            var electricityConsumptionRepositoryMock = new Mock<IElectricityConsumptionRepository>();
            var electricityDataAggregator = new ElectricityDataAggregator();

            electricityConsumptionRepositoryMock.Setup(repo => repo.GetAllDataAsync())
                .ReturnsAsync(new List<ElectricityData>()
                {
                    new ElectricityData(tinkla: "Klaipėdos regiono tinklas",
                                        obtPavadinimas: "Butas",
                                        pMinus: 0.01m,
                                        pPlus: 0.01m),
                    new ElectricityData(tinkla: "Klaipėdos regiono tinklas",
                                        obtPavadinimas: "Butas",
                                        pMinus: 0.01m,
                                        pPlus: 0.01m)
                });

            ElectricityDataController controller = new (dataDownloaderServiceMock.Object,
                                                       electricityConsumptionRepositoryMock.Object,
                                                       electricityDataAggregator);

            var actionResult = await controller.GetAllData();

            actionResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Download_ReturnsOkResult()
        {
            var dataDownloaderServiceMock = new Mock<IDataDownloaderService>();
            var electricityConsumptionRepositoryMock = new Mock<IElectricityConsumptionRepository>();
            var electricityDataAggregator = new ElectricityDataAggregator();

            dataDownloaderServiceMock.Setup(s => s.DownloadData(new()
            {
                "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv",
                "https://data.gov.lt/dataset/1976/download/10763/2022-03.csv",
                "https://data.gov.lt/dataset/1977/download/10763/2022-04.csv",
                "https://data.gov.lt/dataset/1978/download/10763/2022-05.csv"
            }))
            .ReturnsAsync(new List<ElectricityData>()
            {
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas",
                                    obtPavadinimas: "Butas",
                                    pMinus: 0.01m,
                                    pPlus: 0.01m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas",
                                    obtPavadinimas: "Butas",
                                    pMinus: 0.01m,
                                    pPlus: 0.01m)
            });

            electricityConsumptionRepositoryMock.Setup(repo => repo.AddAsync(new List<ElectricityData>()));

            ElectricityDataController controller = new (dataDownloaderServiceMock.Object,
                                                        electricityConsumptionRepositoryMock.Object,
                                                        electricityDataAggregator);

            var actionResult = await controller.DownloadCSV();

            actionResult.Should().BeOfType<OkResult>();
        }
    }
}
