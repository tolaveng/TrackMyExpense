using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;

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
            // income
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

            // income budget jars
            var dbBudgetJars = await _unitOfWork.BudgetJarRepository.GetAllAsync(x => x.UserId == income.UserId);
            var dbIncomeJars = await _unitOfWork.IncomeBudgetJarRepository.GetAllAsync(x => x.IncomeId == income.Id);

            foreach(var requestJar in request.BudgetJars)
            {
                var dbJar = dbBudgetJars.SingleOrDefault(x => x.Id == requestJar.Id);
                var newAmount = Math.Round(income.Amount * decimal.Divide((decimal)requestJar.Percentage, 100), 2, MidpointRounding.AwayFromZero);
                if (dbJar != null)
                {
                    var dbIncomeJar = dbIncomeJars.SingleOrDefault(x => x.IncomeId == income.Id && x.BudgetJarId == dbJar.Id);
                    var oldAmount = dbIncomeJar != null ? dbIncomeJar.Amount : 0;
                    dbJar.TotalBalance = dbJar.TotalBalance - oldAmount + newAmount;
                    _unitOfWork.BudgetJarRepository.Update(dbJar);

                    if (dbIncomeJar != null)
                    {
                        dbIncomeJar.Percentage = requestJar.Percentage;
                        dbIncomeJar.Amount = newAmount;
                        _unitOfWork.IncomeBudgetJarRepository.Update(dbIncomeJar);
                    } else
                    {
                        var newIncomeJar = new IncomeBudgetJar()
                        {
                            IncomeId = income.Id,
                            BudgetJarId = dbJar.Id,
                            Amount = newAmount,
                            Percentage = requestJar.Percentage,
                        };
                        await _unitOfWork.IncomeBudgetJarRepository.InsertAsync(newIncomeJar);
                    }

                } else
                {
                    var newJar = _mapper.Map<BudgetJar>(requestJar);
                    newJar.UserId = income.UserId;
                    newJar.TotalBalance = newAmount;
                    newJar.Icon = null; // Prevent recreate an icon
                    await _unitOfWork.BudgetJarRepository.InsertAsync(newJar);

                    var newIncomeJar = new IncomeBudgetJar()
                    {
                        IncomeId = income.Id,
                        BudgetJarId = newJar.Id,
                        Amount = newAmount,
                        Percentage = requestJar.Percentage,
                    };
                    await _unitOfWork.IncomeBudgetJarRepository.InsertAsync(newIncomeJar);
                }
            }

            await _unitOfWork.SaveAsync();
            return income.Id;
        }
    }
}
