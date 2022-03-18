using Application.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class InsertTickerUseCase : IInsertTickerUseCase
    {
        private ITickerRepository _tickerRepository;

        public InsertTickerUseCase(ITickerRepository tickerRepository)
        {
            _tickerRepository = tickerRepository;
        }
        public async Task InsertTickerAsync(Ticker ticker)
        {
            await _tickerRepository.AddTicker(ticker, Timeseries.RAW);
        }
    }
}
