using Domain;
using Market.Price.Binance.Collector.Service.Events;

namespace Application.UseCases
{
    public static class TickerMapper
    {
        public static Ticker MapToTicker(this ITickerCollected tickerCollected) 
        {
            return new Ticker
            {
                Timestamp = tickerCollected.Timestamp,
                Price = tickerCollected.Price,
                Symbol = tickerCollected.Symbol,
                Volume = Decimal.Parse(tickerCollected.Volume),
                BrokerName = tickerCollected.BrokerName,
            };
        }
    }
}
