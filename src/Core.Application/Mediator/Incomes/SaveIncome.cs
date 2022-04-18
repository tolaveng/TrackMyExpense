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

namespace Core.Application.Mediator.Incomes
{
    public class SaveIncomeRequest : IRequest<Guid>
    {
        public IncomeDto Income { get; set; }
        public BudgetJarDto[] BudgetJars { get; set; }

        public SaveIncomeRequest(IncomeDto income, BudgetJarDto[] budgetJars)
        {
            Income = income;
            BudgetJars = budgetJars;
        }
    }

    public class SaveIncomeRequestHandler : IRequestHandler<SaveIncomeRequest, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SaveIncomeRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveIncomeRequest request, CancellationToken cancellationToken)
        {
            var income = _mapper.Map<Income>(request.Income);
            if (income.Id != Guid.Empty)
            {
                var existIncome = await _unitOfWork.IncomeRepository.GetAsync(x => x.Id == income.Id);
                if (existIncome == null)
                {
                    throw new InvalidOperationException("Income is not found");
                }
                _unitOfWork.IncomeRepository.Update(income);
            }
            else
            {
                income.Id = Guid.NewGuid();
                var result = await _unitOfWork.IncomeRepository.InsertAsync(income);
                if (!result) return Guid.Empty;
            }

            var budgetJars = request.BudgetJars.Select(x => new BudgetJar()
            {
                Id = Guid.NewGuid(),
                IncomeId = income.Id,
                UserId = income.UserId,
                Name = x.Name,
                Amount = x.Amount,
                Percentage = x.Percentage,
                IconId = x.IconId,
        });
            await _unitOfWork.BudgetJarRepository.DeleteAsync(x => x.IncomeId == income.Id);
            await _unitOfWork.BudgetJarRepository.InsertRangeAsync(budgetJars);
            
            await _unitOfWork.SaveAsync();
            return income.Id;
        }
    }
}
