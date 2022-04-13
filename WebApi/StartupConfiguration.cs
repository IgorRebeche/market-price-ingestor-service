using Domain.Repositories;

namespace market_price_ingestor_service
{
    public class StartupConfiguration : IHostedService
    {
        private IConfigurationRepository _configurationRepository;

        public StartupConfiguration(IServiceScopeFactory factory)
        {
            _configurationRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IConfigurationRepository>(); ;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var app = await _configurationRepository.GetAppConfiguration();
            if (app.configurations.Length == 0)
            {
                await _configurationRepository.InsertConfiguration(new Domain.Configuration { Name = "TurnOffSwitch", Description = "Switch para desligar aplicação", Value = "false" });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
