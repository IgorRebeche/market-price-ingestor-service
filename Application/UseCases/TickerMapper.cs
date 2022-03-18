using Domain;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public static class TickerMapper
    {
        public static Ticker MapToTicker(this ITickerCollected tickerCollected) 
        {
            return new Ticker
            {
                Timestamp = tickerCollected.TimeStamp,
                Price = tickerCollected.Price,
                Symbol = tickerCollected.Symbol,
                Volume = Decimal.Parse(tickerCollected.Volume),
                BrokerName = tickerCollected.BrokerName,
            };
        }
    }
}
