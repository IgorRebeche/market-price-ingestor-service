using Domain;

namespace Application.UseCases
{
    public interface IInsertTickerUseCase
    {
        public Task InsertTickerAsync(Ticker ticker);
    }
}
