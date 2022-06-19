using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Mapper;
using Core.Application.Mediator.Incomes;
using Core.Tests.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Incomes
{
    public class DeleteIncomeTests
    {
        private readonly IMapper _mapper;
        private readonly DeleteIncomeHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteIncomeTests()
        {
            _unitOfWorkMock = UnitOfWorkMock.GetMock();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMapperProfile());
            });

            _mapper = mappingConfig.CreateMapper();

            _handler = new DeleteIncomeHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Delete_Income_Should_Update_Total_Balance()
        {
            var request = new DeleteIncome(ConstantMock.IncomeId);
            await _handler.Handle(request, CancellationToken.None);

            var budgetJar1 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId1);
            var budgetJar2 = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == ConstantMock.BudgetJarId2);

            Assert.Equal(0, budgetJar1.TotalBalance);
            Assert.Equal(0, budgetJar2.TotalBalance);
        }
    }
}
