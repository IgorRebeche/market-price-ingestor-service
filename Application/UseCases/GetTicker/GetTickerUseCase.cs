using Application.UseCases.GetTicker.Mappers;
using Application.UseCases.GetTicker.Responses;
using Domain;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetTicker
{
    public class GetTickerUseCase: IGetTickerUseCase
    {
        private ITickerRepository _tickerRepository;
        private ILogger<GetTickerUseCase> _logger;

        public GetTickerUseCase(ILogger<GetTickerUseCase> logger, ITickerRepository tickerRepository)
        {
            _tickerRepository = tickerRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TickerResponse>> GetTickers(string brokerName, string symbol, long timeStampFrom)
        {

            var tickers = await _tickerRepository.GetTickers(brokerName, symbol, timeStampFrom);
            _logger.LogInformation("Found {TickersCount} tickers", tickers.Count());
            
            return tickers.Select(x => x.MapToTickerResponse());
        }
    }
}
