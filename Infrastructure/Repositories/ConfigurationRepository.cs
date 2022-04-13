using Infrastructure.Database.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<Configuration> _appConfigurationCollection;

        public ConfigurationRepository(IOptions<MarketPriceLakeDatabaseConfiguration> mongoConfig)
        {
            var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoConfig.Value.ConfigurationDatabase);
            _appConfigurationCollection = _mongoDatabase.GetCollection<Configuration>(nameof(AppConfiguration));
        }

        public async Task<AppConfiguration> GetAppConfiguration()
        {
            var configurations = await _appConfigurationCollection.Find(_ => true).ToListAsync();

            return new AppConfiguration { configurations = configurations.ToArray() };

        }

        public async Task InsertConfiguration(Configuration configuration)
        {
            await _appConfigurationCollection.InsertOneAsync(configuration);
        }
    }
}
