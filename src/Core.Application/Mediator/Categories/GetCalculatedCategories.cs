using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.Categories
{
    public class GetCalculatedCategories : IRequest<List<CategoryDto>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public GetCalculatedCategories(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }

    public class GetCalculatedCategoriesHandler : IRequestHandler<GetCalculatedCategories, List<CategoryDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetCalculatedCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetCalculatedCategories request, CancellationToken cancellationToken)
        {
            var categories = (await _unitOfWork.ExpenseRepository.GetAllAsync(x => !x.Archived &&
                x.PaidDate >= request.FromDate && x.PaidDate <= request.ToDate, null, new[] { "Category" }))
                .GroupBy(x => x.CategoryId)
                .Select(x => new CategoryDto()
                {
                    Id = x.Key,
                    Name = x.First().Category.Name,
                    TotalAmount = x.Sum(y => y.Amount)
                })
                .ToList();

            return categories;
        }
    }
}
