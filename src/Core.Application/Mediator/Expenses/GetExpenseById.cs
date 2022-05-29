using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Utils;
using MediatR;

namespace Core.Application.Mediator.Expenses
{
    public class GetExpenseById : IRequest<ExpenseDto>
    {
        public string TimeZoneId { get; set; }
        public Guid UserId { get; set; }
        public Guid ExpenseId { get; set; }

        public GetExpenseById(Guid userId, Guid expenseId, string timeZoneId)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            ExpenseId = expenseId;
        }
    }

    public class GetExpenseByIdHandler : IRequestHandler<GetExpenseById, ExpenseDto?>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetExpenseByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ExpenseDto?> Handle(GetExpenseById request, CancellationToken cancellationToken)
        {
            var expense = await _unitOfWork.ExpenseRepository
                .GetAsync(x => x.Id == request.ExpenseId && x.UserId == request.UserId);
            if (expense == null)
            {
                return null;
            }
            expense.PaidDate = DateTimeUtil.ToTimeZoneDateTime(expense.PaidDate, request.TimeZoneId);

            var attachments = await _unitOfWork.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id);
            expense.Attachments = attachments.ToArray();

            return _mapper.Map<ExpenseDto>(expense);
        }
    }
}
