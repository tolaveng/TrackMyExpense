using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Mediator.BudgetJars
{
    public class SaveBudgetJarCommand : IRequest<Guid>
    {
        public BudgetJarDto BudgetJarDto { get; set; }
        public SaveBudgetJarCommand(BudgetJarDto _budgetJarDto)
        {
            BudgetJarDto = _budgetJarDto;
        }
    }

    public class SaveBudgetJarHandler : IRequestHandler<SaveBudgetJarCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveBudgetJarHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveBudgetJarCommand request, CancellationToken cancellationToken)
        {
            var budgetJar = _mapper.Map<BudgetJar>(request.BudgetJarDto);
            if (budgetJar.Id == Guid.Empty)
            {
                budgetJar.Id = Guid.NewGuid();
                budgetJar.Icon = null;  // prevent adding new icon
                await _unitOfWork.BudgetJarRepository.InsertAsync(budgetJar);
            }
            else
            {
                _unitOfWork.BudgetJarRepository.Update(budgetJar);
            }

            await _unitOfWork.SaveAsync();

            return budgetJar.Id;
        }
    }
}
