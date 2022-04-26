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
        public async Task<Ticker> AddTicker(Ticker ticker)
        {
            var collectionName = $"{ticker.BrokerName}.{ticker.Symbol}";
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

        public async Task<IEnumerable<Ticker>> GetTickers(string brokerName, string symbol, long timeStampFrom)
        {
            var collectionName = $"{brokerName}.{symbol}";
            _tickersCollection = _mongoDatabase.GetCollection<Ticker>(collectionName);

            var tickers = await _tickersCollection.FindAsync(ticker => ticker.Timestamp >= timeStampFrom).ConfigureAwait(false);

            return tickers.ToList();
        }

        public async Task<IEnumerable<Ticker>> GetTickersRange(string brokerName, string symbol, long timeStampFrom, long timeStampTo)
        {
            var collectionName = $"{brokerName}.{symbol}";
            _tickersCollection = _mongoDatabase.GetCollection<Ticker>(collectionName);
            var filterBuilder = Builders<Ticker>.Filter;
            var filter = filterBuilder.Gte(x => x.Timestamp, timeStampFrom) & filterBuilder.Lte(x => x.Timestamp, timeStampTo);

            var tickers = await _tickersCollection
                .Find(filter)
                .SortBy(x => x.Timestamp)
                .ToListAsync()
                .ConfigureAwait(false);

            return tickers.ToList();
        }
    }
}
