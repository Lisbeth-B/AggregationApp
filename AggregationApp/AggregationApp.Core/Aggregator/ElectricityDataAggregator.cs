namespace AggregationApp.Core.Filter
{
    public class ElectricityDataAggregator : IElectricityDataAggregator
    {
        public List<ElectricityData> AggregateData(List<ElectricityData> data, string obtPavadinimas)
        {
            return data.Where(d => d.ObtPavadinimas == obtPavadinimas)
                       .GroupBy(d => d.Tinkla)
                       .Select(g => new ElectricityData
                       (
                           tinkla: g.First().Tinkla,
                           obtPavadinimas: g.First().ObtPavadinimas,
                           pMinus: g.Sum(s => s.PMinus),
                           pPlus: g.Sum(s => s.PPlus)
                       ))
                       .ToList();
        }
    }
}