using Application.UseCases.GetTicker.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GetTicker.Mappers
{
    public static class TickerResponseMapper
    {
        public static TickerResponse MapToTickerResponse(this Ticker ticker)
        {
            return new TickerResponse
            {
                Timestamp = ticker.Timestamp,
                Price = ticker.Price,
                Symbol = ticker.Symbol,
                Volume = ticker.Volume,
                BrokerName = ticker.BrokerName,
            };
        }
    }
}
