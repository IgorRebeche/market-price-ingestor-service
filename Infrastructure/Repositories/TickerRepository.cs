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

        public TickerRepository(IOptions<MarketPriceLakeDatabaseConfiguration> mongoConfig)
        {
            var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoConfig.Value.DatabaseName);
            _tickersCollection = mongoDatabase.GetCollection<Ticker>("Tickers");
        }
        public async Task<Ticker> AddTicker(Ticker ticker)
        {
            await _tickersCollection.InsertOneAsync(ticker);
            
            return ticker;
        }
    }
}
