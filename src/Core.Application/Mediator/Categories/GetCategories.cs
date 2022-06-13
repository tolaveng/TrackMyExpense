using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Categories
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public bool IsSystem { get; set; }
        public Guid? UserId { get; set; }
        public GetCategoriesQuery(bool isSystem, Guid? userId = null)
        {
            IsSystem = isSystem;
            UserId = userId;
        }
    }
    public class GetCategoryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetCategoryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.CategoryRepository;
            var categories = await repo.GetAllAsync(x => (x.IsSystem || !request.IsSystem) &&
            (!request.UserId.HasValue || x.UserId == request.UserId),
                x => x.OrderBy(o => o.Name), new [] {"Icon"});
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
