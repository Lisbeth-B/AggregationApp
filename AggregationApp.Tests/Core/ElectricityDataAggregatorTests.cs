using AggregationApp.Core;
using AggregationApp.Core.Filter;
using FluentAssertions;

namespace AggregationApp.Tests.Core
{
    public class ElectricityDataAggregatorTests
    {
        IElectricityDataAggregator electricityDataAggregator = new ElectricityDataAggregator();

        [Fact]
        public void AggregateDataTest_EmptyList()
        {

            List<ElectricityData> data = new()
            {
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
            };

            var aggregatedDataList = electricityDataAggregator.AggregateData(data, "Namas");

            aggregatedDataList.Count.Should().Be(0);
        }

        [Fact]
        public void AggregateData_ListWithOneData()
        {
            List<ElectricityData> data = new()
            {
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.05m),
            };

            var aggregatedDataList = electricityDataAggregator.AggregateData(data, "Butas");

            aggregatedDataList.Count.Should().Be(1);

            ElectricityData aggregatedData = aggregatedDataList.First();

            aggregatedData.Tinkla.Should().Be("Klaipėdos regiono tinklas");
            aggregatedData.ObtPavadinimas.Should().Be("Butas");
            aggregatedData.PPlus.Should().Be(0.06m);
            aggregatedData.PMinus.Should().Be(0.3m);
        }

        [Fact]
        public void AggregateDataTest_ListWithSeveralDatas()
        {
            List<ElectricityData> data = new()
            {
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.01m, pMinus: 0.04m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.02m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Alytaus regiono tinklas", obtPavadinimas: "Namas", pPlus: 0.01m, pMinus: 0.06m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.02m, pMinus: 0.05m),
                new ElectricityData(tinkla: "Klaipėdos regiono tinklas", obtPavadinimas: "Namas", pPlus: 0.01m, pMinus: 0.02m),
                new ElectricityData(tinkla: "Alytaus regiono tinklas", obtPavadinimas: "Butas", pPlus: 0.02m, pMinus: 0.05m),
            };

            List<ElectricityData> aggregatedDataList = electricityDataAggregator.AggregateData(data, "Butas");

            aggregatedDataList.Count.Should().Be(2);

            var aggregatedData = aggregatedDataList.Where(d => d.Tinkla == "Klaipėdos regiono tinklas").First();

            aggregatedData.Tinkla.Should().Be("Klaipėdos regiono tinklas");
            aggregatedData.ObtPavadinimas.Should().Be("Butas");
            aggregatedData.PPlus.Should().Be(0.05m);
            aggregatedData.PMinus.Should().Be(0.14m);

            aggregatedData = aggregatedDataList.Where(d => d.Tinkla == "Alytaus regiono tinklas").First();

            aggregatedData.Tinkla.Should().Be("Alytaus regiono tinklas");
            aggregatedData.ObtPavadinimas.Should().Be("Butas");
            aggregatedData.PPlus.Should().Be(0.02m);
            aggregatedData.PMinus.Should().Be(0.05m);
        }
    }
}