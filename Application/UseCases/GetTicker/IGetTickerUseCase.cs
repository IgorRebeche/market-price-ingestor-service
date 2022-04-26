using Application.UseCases.GetTicker.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GetTicker
{
    public interface IGetTickerUseCase
    {
        public Task<IEnumerable<TickerResponse>> GetTickers(string brokerName, string symbol, long timeStampFrom);
    }
}
