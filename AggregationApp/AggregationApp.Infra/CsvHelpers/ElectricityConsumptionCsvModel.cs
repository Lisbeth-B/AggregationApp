using CsvHelper.Configuration.Attributes;

namespace AggregationApp.Infra.CsvHelpers
{
    public class ElectricityConsumptionCsvModel
    {
        [Name("TINKLAS")]
        public string Tinkla { get; set; }

        [Name("OBT_PAVADINIMAS")]
        public string ObtPavadinimas { get; set; }

        [Name("OBJ_GV_TIPAS")]
        public string ObjGvTipas { get; set; }

        [Name("OBJ_NUMERIS")]
        public int ObjNumeris { get; set; }

        [Name("P+")]
        public decimal? PPlus { get; set; }

        [Name("PL_T")]
        public DateTime PLT { get; set; }

        [Name("P-")]
        public decimal? PMinus { get; set; }
    }
}
