using Core.Application.IRepositories;
using Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Repositories
{
    public static class IncomeRepositoryMock
    {
        public static Mock<IGenericRepository<Income>> GetRepository()
        {
            var incomes = new List<Income>();

            incomes.Add(new Income()
            {
                Id = ConstantMock.IncomeId,
                UserId = ConstantMock.UserId,
                Amount = 200,
            });

            // amount is devided in 2 jars
            var incomeBudgetJars = new List<IncomeBudgetJar>();
            incomeBudgetJars.Add(new IncomeBudgetJar()
            {
                IncomeId = ConstantMock.IncomeId,
                BudgetJarId = ConstantMock.BudgetJarId1,
                Amount = 140,
            });

            incomeBudgetJars.Add(new IncomeBudgetJar()
            {
                IncomeId = ConstantMock.IncomeId,
                BudgetJarId = ConstantMock.BudgetJarId2,
                Amount = 60,
            });

            var repo = new Mock<IGenericRepository<Income>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(incomes);

            repo.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Income, bool>>>(),
                It.IsAny<Func<IQueryable<Income>, IOrderedQueryable<Income>>?>(),
                It.IsAny<string[]?>()))
                .Returns((Expression<Func<Income, bool>> expression,
                Func<IQueryable<Income>, IOrderedQueryable<Income>>? orderBy,
                string[]? includes) =>
                {
                    return Task.FromResult(incomes.Where(expression.Compile().Invoke));
                });

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Income, bool>>>(), It.IsAny<string[]?>()))
                .Returns(async (Expression<Func<Income, bool>> expression, string[]? includes) =>
                {
                    var income = incomes.FirstOrDefault(expression.Compile().Invoke);
                    if (income == null) throw new ArgumentException("Income not found.");
                    return await Task.FromResult(income);
                });

            repo.Setup(x => x.Update(It.IsAny<Income>())).Returns((Income income) =>
            {
                var aIncome = incomes.Single(x => x.Id == income.Id);
                aIncome.Amount = income.Amount;
                
                return true;
            });

            repo.Setup(x => x.InsertAsync(It.IsAny<Income>())).Returns((Income income) =>
            {
                return Task.FromResult(true);
            });

            return repo;
        }
    }
}
