using Application.Repositories;
using Domain;
using Infrastructure.Database.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TickerRepository : ITickerRepository
    {
        private IMongoCollection<Ticker> _tickersCollection;
        private IMongoDatabase _mongoDatabase;

        public TickerRepository(IOptions<MarketPriceLakeDatabaseConfiguration> mongoConfig)
        {
            var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoConfig.Value.DatabaseName);
        }
        public async Task<Ticker> AddTicker(Ticker ticker, Timeseries timeseries)
        {
            var collectionName = $"{ticker.BrokerName}.{ticker.Symbol}.{timeseries}";
            _tickersCollection = _mongoDatabase.GetCollection<Ticker>(collectionName);
            await _tickersCollection.InsertOneAsync(ticker);
            
            return ticker;
        }
    }
}
