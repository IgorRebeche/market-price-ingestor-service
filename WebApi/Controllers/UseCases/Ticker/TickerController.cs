using Application.UseCases.GetTicker;
using Application.UseCases.GetTicker.Responses;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace market_price_ingestor_service.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TickerController : ControllerBase
    {
        private ILogger<TickerController> _logger;
        private IGetTickerUseCase _getTickerUseCase;

        public TickerController(ILogger<TickerController> logger, IGetTickerUseCase getTickerUseCase)
        {
            _logger = logger;
            _getTickerUseCase = getTickerUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string brokerName, [FromQuery] string symbol, [FromQuery] long timeStampFrom)
        {
            var tickers  = await _getTickerUseCase.GetTickers(brokerName, symbol, timeStampFrom);
            return Ok(tickers);
        }

    }
}
