using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<CurrencyDto>> GetAll()
        {
            var currencies = await _unitOfWork.CurrencyRepository.GetAllAsync(null, x => x.OrderBy(z => z.Code));
            
            return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
        }
    }
}
