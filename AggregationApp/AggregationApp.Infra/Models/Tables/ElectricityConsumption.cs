namespace AggregationApp.Infra.Models.Tables
{
    public class ElectricityConsumption
    {
        public int Id { get; set; }
        public string Tinkla { get; set; }
        public string ObtPavadinimas { get; set; }
        public decimal PPlus { get; set; }
        public decimal PMinus { get; set; }
    }
}
