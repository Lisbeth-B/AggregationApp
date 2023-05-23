namespace AggregationApp.Core
{
    public class ElectricityData
    {
        public ElectricityData(string tinkla, string obtPavadinimas, decimal? pPlus, decimal? pMinus)
        {
            Tinkla = tinkla;
            ObtPavadinimas = obtPavadinimas;
            PPlus = pPlus ?? 0;
            PMinus = pMinus ?? 0;
        }

        public string Tinkla { get; set; }
        public string ObtPavadinimas { get; set; }
        public decimal PPlus { get; set; }
        public decimal PMinus { get; set; }
    }
}
