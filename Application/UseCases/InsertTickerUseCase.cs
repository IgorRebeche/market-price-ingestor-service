using Domain;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    public class InsertTickerUseCase : IInsertTickerUseCase
    {
        private ITickerRepository _tickerRepository;
        private IConfigurationRepository _configurationRepository;
        private ILogger<InsertTickerUseCase> _logger;

        public InsertTickerUseCase(ILogger<InsertTickerUseCase> logger,ITickerRepository tickerRepository, IConfigurationRepository configurationRepository)
        {
            _tickerRepository = tickerRepository;
            _configurationRepository = configurationRepository;
            _logger = logger;
        }
        public async Task InsertTickerAsync(Ticker ticker)
        {
            var cannotOperate = !await CanOperate();

            if (cannotOperate) throw new Exception("Application Disabled, cant operate!");
                
            await _tickerRepository.AddTicker(ticker, Timeseries.RAW);
        }

        private async Task<bool> CanOperate()
        {
            var config = await _configurationRepository.GetAppConfiguration();
            var turnOffSwitch = config.GetConfiguration("TurnOffSwitch");

            if (turnOffSwitch is null)
            {
                _logger.LogInformation("Configuration is null");
                return false;
            }

            if (turnOffSwitch.Value == "true")
            {
                _logger.LogInformation("Application Disabled to operate");
                return false;
            }

            return true;
        }
    }
}
