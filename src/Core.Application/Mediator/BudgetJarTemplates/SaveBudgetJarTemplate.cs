﻿using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Mediator.BudgetJarTemplates
{
    public class SaveBudgetJarTemplateCommand : IRequest<Guid>
    {
        public BudgetJarTemplateDto BudgetJarDto { get; set; }
        public bool IsNew { get; set; }
        public SaveBudgetJarTemplateCommand(BudgetJarTemplateDto _budgetJarDto, bool isNew = false)
        {
            BudgetJarDto = _budgetJarDto;
            IsNew = isNew;
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
            if (budgetJar.Id != Guid.Empty && !request.IsNew)
            {
                var exist = await _unitOfWork.BudgetJarTemplateRepository.GetAsync(x => x.Id == budgetJar.Id);
                if (exist == null)
                {
                    throw new InvalidOperationException("Budget jar template is not found");
                }
                budgetJar.Icon = null;
                _unitOfWork.BudgetJarTemplateRepository.Update(budgetJar);
            }
            else
            {
                if (budgetJar.Id == Guid.Empty)
                {
                    budgetJar.Id = Guid.NewGuid();
                }
                budgetJar.Icon = null;
                await _unitOfWork.BudgetJarTemplateRepository.InsertAsync(budgetJar);
            }

            await _unitOfWork.SaveAsync();

            return budgetJar.Id;
        }
    }
}
