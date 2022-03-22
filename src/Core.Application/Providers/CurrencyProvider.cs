using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
using Core.Domain.Entities;


namespace Core.Application.Providers
{
    public class CurrencyProvider : ICurrencyProvider
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public CurrencyProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CurrencyDto>> GetAll(string? orderBy = null)
        {
            Func<IQueryable<Currency>, IOrderedQueryable<Currency>> ordered = (order) =>
            {
                if (orderBy != null && orderBy.Equals("Text", StringComparison.OrdinalIgnoreCase))
                {
                    return order.OrderBy(x => x.Text);
                }
                return order.OrderBy(x => x.Code);
            };

            var currencies = await _unitOfWork.CurrencyRepository.GetAllAsync(null, ordered);
            
            return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
        }
    }
}
