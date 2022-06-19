using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Mapper;
using Core.Application.Mediator.Expenses;
using Core.Tests.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Expenses
{
    public class DeleteExpenseTests
    {
        private readonly IMapper _mapper;
        private readonly DeleteExpenseHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteExpenseTests()
        {
            _unitOfWorkMock = UnitOfWorkMock.GetMock();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMapperProfile());
            });

            _mapper = mappingConfig.CreateMapper();

            _handler = new DeleteExpenseHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Delete_Expense_Should_Update_Total_Balance()
        {
            var request = new DeleteExpense(ConstantMock.ExpenseId);
            await _handler.Handle(request, CancellationToken.None);

            var budgetJar1 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId1);
            var budgetJar2 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId2);

            Assert.Equal(165, budgetJar1.TotalBalance); // 140 + 25
            Assert.Equal(60, budgetJar2.TotalBalance);  // 60 + 0
        }
    }
}
