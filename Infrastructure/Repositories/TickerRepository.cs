using Domain;
using Domain.Repositories;
using Infrastructure.Database.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class TickerRepository : ITickerRepository
    {
        private IMongoCollection<Ticker> _tickersCollection;
        private IMongoDatabase _mongoDatabase;

        public TickerRepository(IOptions<MarketPriceLakeDatabaseConfiguration> mongoConfig)
        {
            var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoConfig.Value.Database);
        }
        public async Task<Ticker> AddTicker(Ticker ticker, Timeseries timeseries)
        {
            var collectionName = $"{ticker.BrokerName}.{ticker.Symbol}.{timeseries}";
            _tickersCollection = _mongoDatabase.GetCollection<Ticker>(collectionName);
            
            var options = new CreateIndexOptions { Unique = true };
            var indexKeysDefine = Builders<Ticker>.IndexKeys
                .Ascending(indexKey => indexKey.Timestamp)
                .Ascending(indexKey => indexKey.BrokerName)
                .Ascending(indexKey => indexKey.Symbol)
                .Ascending(indexKey => indexKey.Price);
            var indexModel = new CreateIndexModel<Ticker>(indexKeysDefine, options);
            _ = await _tickersCollection.Indexes.CreateOneAsync(indexModel);
            
            await _tickersCollection.InsertOneAsync(ticker);

            
            return ticker;
        }
    }
}
