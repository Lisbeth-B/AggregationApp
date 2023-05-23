using AggregationApp.Infra.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AggregationApp.Infra.Models
{
    public class AggregationAppContext : DbContext
    {
        public AggregationAppContext()
        {

        }

        public AggregationAppContext(DbContextOptions<AggregationAppContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect())
                    {
                        databaseCreator.Create();
                    }

                    if (!databaseCreator.HasTables())
                    {
                        databaseCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public DbSet<ElectricityConsumption> ElectricityConsumptions { get; set; }
    }
}
