using Application.Repositories;
using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consumers
{
    public class TickerCollectedConsumer :
            IConsumer<ITickerCollected>
    {
        private ILogger<TickerCollectedConsumer> _logger;
        private ITickerRepository _tickerRepository;

        public TickerCollectedConsumer(ILogger<TickerCollectedConsumer> logger, ITickerRepository tickerRepository)
        {
            _logger = logger;
            _tickerRepository = tickerRepository;
        }

        public async Task Consume(ConsumeContext<ITickerCollected> context)
        {
            _logger.LogInformation("Msg Consumed: {@0}", context.Message);

            await _tickerRepository.AddTicker(new Domain.Ticker { Price=1, TimeStamp= DateTime.Now });

        }
    }
}
