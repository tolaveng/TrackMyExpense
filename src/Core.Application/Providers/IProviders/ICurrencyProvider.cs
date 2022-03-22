using Core.Application.Models;

namespace Core.Application.Providers.IProviders
{
    public interface ICurrencyProvider
    {
        Task<IEnumerable<CurrencyDto>> GetAll(string orderBy);
    }
}
