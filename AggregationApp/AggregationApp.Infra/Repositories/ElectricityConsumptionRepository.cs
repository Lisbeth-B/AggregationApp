using AggregationApp.Core;
using AggregationApp.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AggregationApp.Infra.Repositories
{
    public class ElectricityConsumptionRepository : IElectricityConsumptionRepository
    {
        private readonly AggregationAppContext _dbContext;
        private readonly ILogger<ElectricityConsumptionRepository> _logger;

        public ElectricityConsumptionRepository(AggregationAppContext context, ILogger<ElectricityConsumptionRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task AddAsync(List<ElectricityData> data)
        {
            IEnumerable<Models.Tables.ElectricityConsumption> aggregatedData = data
                .Select(g => new Models.Tables.ElectricityConsumption
                {
                    Tinkla = g.Tinkla,
                    ObtPavadinimas = g.ObtPavadinimas,
                    PMinus = g.PMinus,
                    PPlus = g.PPlus
                });

            try
            {
                await _dbContext.ElectricityConsumptions.AddRangeAsync(aggregatedData);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Could not add data into the database");
            }
        }

        public async Task<List<ElectricityData>> GetAllDataAsync()
        {
            List<Models.Tables.ElectricityConsumption> modelData = await _dbContext.ElectricityConsumptions.ToListAsync();

            if (modelData.Count == 0)
            {
                _logger.LogInformation("Database is empty. Please download information first");
            }

            return modelData.Select(c => new ElectricityData
            (
                tinkla: c.Tinkla,
                obtPavadinimas: c.ObtPavadinimas,
                pMinus: c.PMinus,
                pPlus: c.PPlus
            )).ToList();
        }

    }
}