using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Utils;
using MediatR;


namespace Core.Application.Mediator.Incomes
{
    public class GetIncomeById : IRequest<IncomeDto>
    {
        public string TimeZoneId { get; set; }
        public Guid UserId { get; set; }
        public Guid IncomeId { get; set; }

        public GetIncomeById(Guid userId, Guid incomeId, string timeZoneId)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            IncomeId = incomeId;
        }
    }

    public class GetIncomeByIdHandler : IRequestHandler<GetIncomeById, IncomeDto?>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetIncomeByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IncomeDto?> Handle(GetIncomeById request, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.IncomeRepository.GetAsync(x => x.Id == request.IncomeId && x.UserId == request.UserId);
            if (income == null)
            {
                return null;
            }
            income.FromDate = DateTimeUtil.ToTimeZoneDateTime(income.FromDate, request.TimeZoneId);
            income.ToDate = DateTimeUtil.ToTimeZoneDateTime(income.ToDate, request.TimeZoneId);
            return _mapper.Map<IncomeDto>(income);
        }
    }
}
