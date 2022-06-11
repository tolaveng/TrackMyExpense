using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Mapper;
using Core.Application.Mediator.Incomes;
using Core.Application.Models;
using Core.Domain.Entities;
using Core.Tests.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Expenses
{
    public class SaveIncomeTests
    {
        private readonly IMapper _mapper;
        private readonly SaveIncomeRequestHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public SaveIncomeTests()
        {
            _unitOfWorkMock = UnitOfWorkMock.GetMock();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _handler = new SaveIncomeRequestHandler(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async void SaveIncome_Should_Save_New_Income()
        {
            var income = new IncomeDto()
            {
                UserId = ConstantMock.UserId,
                Id = Guid.Empty,
                Amount = 25,
            };

            var budgetJars = new List<BudgetJarDto>();
            budgetJars.Add(new BudgetJarDto() {
                Id = ConstantMock.BudgetJarId1,
                Percentage = 50,
                UserId = ConstantMock.UserId,
            });

            budgetJars.Add(new BudgetJarDto()
            {
                Id = ConstantMock.BudgetJarId2,
                Percentage = 50,
                UserId = ConstantMock.UserId,
            });

            var request = new SaveIncomeRequest(income, budgetJars.ToArray());
            var guid = await _handler.Handle(request, CancellationToken.None);
            var budgetJar1 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId1);
            var budgetJar2 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId2);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.Equal(152.5m, budgetJar1.TotalBalance); // 140 + (25 / 2)
            Assert.Equal(72.5m, budgetJar2.TotalBalance); // 60 + 12.5
        }

        [Fact]
        public async void SaveIncome_Update_Amount_Should_Update_TotalBalance()
        {
            var income = (await _unitOfWorkMock.Object.IncomeRepository.GetAllAsync()).First();
            var incomeDto = _mapper.Map<Income, IncomeDto>(income);
            incomeDto.Amount = 100;

            var incomeBudgetJars = await _unitOfWorkMock.Object.IncomeBudgetJarRepository
                .GetAllAsync(x => x.IncomeId == income.Id);
            var budgetJarIds = incomeBudgetJars.Select(x => x.BudgetJarId).ToArray();
            var budgetJars = await _unitOfWorkMock.Object.BudgetJarRepository
                .GetAllAsync(x => budgetJarIds.Contains(x.Id));
            var budgetJarsDto = _mapper.Map<BudgetJar[], BudgetJarDto[]>(budgetJars.ToArray());


            var request = new SaveIncomeRequest(incomeDto, budgetJarsDto);
            var guid = await _handler.Handle(request, CancellationToken.None);
            var budgetJar1 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId1);
            var budgetJar2 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId2);
            var incomeJar1 = await _unitOfWorkMock.Object.IncomeBudgetJarRepository.GetAsync(x => x.BudgetJarId == budgetJar1.Id);
            var incomeJar2 = await _unitOfWorkMock.Object.IncomeBudgetJarRepository.GetAsync(x => x.BudgetJarId == budgetJar2.Id);


            // new income, jar 1: 70, jar 2: 30
            // exist income: 200, jar 1: 140, jar 2, 60

            Assert.NotEqual(Guid.Empty, guid);
            Assert.Equal(70, budgetJar1.TotalBalance); // 140 - 140 + 70
            Assert.Equal(30, budgetJar2.TotalBalance); // 60 - 60 + 30
            Assert.Equal(70, incomeJar1.Amount);
            Assert.Equal(30, incomeJar2.Amount);
        }

    }
}
