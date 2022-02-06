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
    public class SaveBudgetJarTemplateCommand : IRequest<Guid>
    {
        public BudgetJarTemplateDto BudgetJarDto { get; set; }
        public SaveBudgetJarTemplateCommand(BudgetJarTemplateDto _budgetJarDto)
        {
            BudgetJarDto = _budgetJarDto;
        }
    }

    public class SaveBudgetJarTemplateHandler : IRequestHandler<SaveBudgetJarTemplateCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveBudgetJarTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveBudgetJarTemplateCommand request, CancellationToken cancellationToken)
        {
            var budgetJar = _mapper.Map<BudgetJarTemplate>(request.BudgetJarDto);
            if (budgetJar.Id == Guid.Empty)
            {
                budgetJar.Id = Guid.NewGuid();
                budgetJar.Icon = null;
                await _unitOfWork.BudgetJarTemplateRepository.InsertAsync(budgetJar);
            }
            else
            {
                _unitOfWork.BudgetJarTemplateRepository.Update(budgetJar);
            }

            await _unitOfWork.SaveAsync();

            return budgetJar.Id;
        }
    }
}
