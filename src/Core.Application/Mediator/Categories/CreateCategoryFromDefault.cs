using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;


namespace Core.Application.Mediator.Categories
{
    public class CreateCategoryFromDefault : IRequest<List<CategoryDto>>
    {
        public Guid UserId { get; set; }
        public CreateCategoryFromDefault(Guid userId)
        {
            UserId = userId;
        }
    }
    public class CreateCategoryFromDefaultHander : IRequestHandler<CreateCategoryFromDefault, List<CategoryDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public CreateCategoryFromDefaultHander(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryDto>> Handle(CreateCategoryFromDefault request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.CategoryRepository;
            var userCategories = await repo.GetAllAsync(x => !x.IsSystem && !x.Archived && x.UserId == request.UserId
                , null, new[] { "Icon" });

            if (!userCategories.Any())
            {
                var sysCategories = await repo.GetAllAsync(x => x.IsSystem && !x.Archived, null, new[] { "Icon" });
                userCategories = sysCategories.Select(z => new Category()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    IsSystem = false,
                    Name = z.Name,
                    IconId = z.IconId,
                    Icon = z.Icon,
                    Archived = false
                });

                await repo.InsertRangeAsync(userCategories);
                await _unitOfWork.SaveAsync();
            }
            return _mapper.Map<List<CategoryDto>>(userCategories);
        }
    }
}
