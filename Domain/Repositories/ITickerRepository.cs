using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITickerRepository
    {
        public Task<Ticker> AddTicker(Ticker ticker);
        public Task<IEnumerable<Ticker>> GetTickers(string brokerName, string symbol, long timeStampFrom);
        public Task<IEnumerable<Ticker>> GetTickersRange(string brokerName, string symbol, long timeStampFrom, long timeStampTo);
    }
}
