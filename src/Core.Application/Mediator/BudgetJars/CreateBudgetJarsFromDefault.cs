using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.BudgetJars
{
    public class CreateBudgetJarsFromDefault : IRequest<List<BudgetJarDto>>
    {
        public Guid UserId { get; set; }
        public CreateBudgetJarsFromDefault(Guid userId)
        {
            UserId = userId;
        }
    }
    public class CreateBudgetJarsFromDefaultHander : IRequestHandler<CreateBudgetJarsFromDefault, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public CreateBudgetJarsFromDefaultHander(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BudgetJarDto>> Handle(CreateBudgetJarsFromDefault request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarRepository;
            var userBudgetJars = await repo.GetAllAsync(x => !x.IsDefault && !x.Archived && x.UserId == request.UserId
                , null, new[] { "Icon" });

            if (!userBudgetJars.Any())
            {
                var systemTemplates = await repo.GetAllAsync(x => x.IsDefault && !x.Archived, null, new[] { "Icon" });
                userBudgetJars = systemTemplates.Select(z => new BudgetJar()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Name = z.Name,
                    Percentage = z.Percentage,
                    TotalBalance = 0,
                    IsDefault = false,
                    IconId = z.IconId,
                    Icon = z.Icon,
                });

                await repo.InsertRangeAsync(userBudgetJars);
                await _unitOfWork.SaveAsync();
            }
            return _mapper.Map<List<BudgetJarDto>>(userBudgetJars);
        }
    }
}
