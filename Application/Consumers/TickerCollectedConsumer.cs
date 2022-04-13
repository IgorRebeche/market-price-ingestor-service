using Application.UseCases;
using MassTransit;
using Microsoft.Extensions.Logging;
using Market.Price.Binance.Collector.Service.Events;

namespace Application.Consumers
{
    public class TickerCollectedConsumer :
            IConsumer<ITickerCollected>
    {
        private ILogger<TickerCollectedConsumer> _logger;
        private IInsertTickerUseCase _tickerCollectedUseCase;

        public TickerCollectedConsumer(ILogger<TickerCollectedConsumer> logger, IInsertTickerUseCase tickerCollectedUseCase)
        {
            _logger = logger;
            _tickerCollectedUseCase = tickerCollectedUseCase;
        }

        public async Task Consume(ConsumeContext<ITickerCollected> context)
        {
            _logger.LogInformation("Msg Consumed: {@0}", context.Message);

            await _tickerCollectedUseCase.InsertTickerAsync(context.Message.MapToTicker());

        }
    }
}
