using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace Core.Application.Mediator.Icons
{
    public class GetIconsPaged : IRequest<PagedResponse<IconDto>>
    {
        public Pagination Pagination { get; set; } = new Pagination();
        public bool IncludeArchived { get; set; }
        public GetIconsPaged(bool includeArchived = false)
        {
            IncludeArchived = includeArchived;
        }
    }
    public class GetIconsPagedHandler : IRequestHandler<GetIconsPaged, PagedResponse<IconDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IFileDirectoryProvider _fileDirectoryProvider;
        public GetIconsPagedHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileDirectoryProvider fileDirectoryProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileDirectoryProvider = fileDirectoryProvider;
        }
        public async Task<PagedResponse<IconDto>> Handle(GetIconsPaged request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.IconRepository;
            var pagination = request.Pagination;

            Func<IQueryable<Icon>, IOrderedQueryable<Icon>> orderBy = x => x.OrderByDescending(z => z.Name);
            switch (pagination.SortBy)
            {
                case "Name":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Name)
                    : orderBy = x => x.OrderByDescending(z => z.Name);
                    break;

                case "Path":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Path)
                    : orderBy = x => x.OrderByDescending(z => z.Path);
                    break;

                case "Type":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.IconType)
                    : orderBy = x => x.OrderByDescending(z => z.IconType);
                    break;
            }
            Expression<Func<Icon, bool>> expression = (z) => !z.Archived;
            var count = await repo.CountAsync(expression);
            var data = await repo.GetPagedAsync(pagination.Page, pagination.PageSize, expression, orderBy);
            
            return new PagedResponse<IconDto>(_mapper.Map<IEnumerable<IconDto>>(data), count);
        }
    }
}
