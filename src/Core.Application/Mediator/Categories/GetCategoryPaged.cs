using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace Core.Application.Mediator.Categories
{
    public class GetCategoryPaged : IRequest<PagedResponse<CategoryDto>>
    {
        public Guid UserId { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();

        public string Filter { get; set; }

        public GetCategoryPaged(Guid userId, Pagination pagination, string filter)
        {
            UserId = userId;
            Pagination = pagination;
            Filter = filter;
        }
    }

    public class GetCategoryPagedHandler : IRequestHandler<GetCategoryPaged, PagedResponse<CategoryDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetCategoryPagedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<CategoryDto>> Handle(GetCategoryPaged request, CancellationToken cancellationToken)
        {
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = x => x.OrderBy(z => z.Name);

            if (!string.IsNullOrWhiteSpace(request.Pagination.SortBy) && request.Pagination.SortBy == "Name")
            {
                orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Name)
                    : orderBy = x => x.OrderByDescending(z => z.Name);
            }

            Expression<Func<Category, bool>> expression = x => x.UserId == request.UserId && !x.Archived;
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                expression = x => x.UserId == request.UserId && !x.Archived &&
                    x.Name.ToUpper().Contains(request.Filter.ToUpper());
            }

            var count = await _unitOfWork.CategoryRepository.CountAsync(expression);
            var data = await _unitOfWork.CategoryRepository.GetPagedAsync(request.Pagination.Page, request.Pagination.PageSize,
                expression, orderBy, new[] { "Icon" });

            return new PagedResponse<CategoryDto>(_mapper.Map<IEnumerable<CategoryDto>>(data), count);
        }
    }
}
