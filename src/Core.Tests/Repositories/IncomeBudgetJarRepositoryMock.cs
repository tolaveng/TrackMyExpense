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
    public static class IncomeBudgetJarRepositoryMock
    {
        public static Mock<IGenericRepository<IncomeBudgetJar>> GetRepository()
        {
            var incomeBudgetJars = new List<IncomeBudgetJar>();

            incomeBudgetJars.Add(new IncomeBudgetJar() {
                Amount = 140,
                IncomeId = ConstantMock.IncomeId,
                BudgetJarId = ConstantMock.BudgetJarId1
            });

            incomeBudgetJars.Add(new IncomeBudgetJar()
            {
                Amount = 60,
                IncomeId = ConstantMock.IncomeId,
                BudgetJarId = ConstantMock.BudgetJarId2
            });


            var repo = new Mock<IGenericRepository<IncomeBudgetJar>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(incomeBudgetJars);

            repo.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<IncomeBudgetJar, bool>>>(),
                It.IsAny<Func<IQueryable<IncomeBudgetJar>, IOrderedQueryable<IncomeBudgetJar>>?>(),
                It.IsAny<string[]?>()))
                .Returns((Expression<Func<IncomeBudgetJar, bool>> expression,
                Func<IQueryable<IncomeBudgetJar>, IOrderedQueryable<IncomeBudgetJar>>? orderBy,
                string[]? includes) =>
                {
                    return Task.FromResult(incomeBudgetJars.Where(expression.Compile().Invoke));
                });

            repo.Setup(x => x.InsertAsync(It.IsAny<IncomeBudgetJar>())).Returns((IncomeBudgetJar incomeJar) =>
            {
                incomeBudgetJars.Add(incomeJar);
                return Task.FromResult(true);
            });

            repo.Setup(x => x.Update(It.IsAny<IncomeBudgetJar>())).Returns((IncomeBudgetJar income) =>
            {
                var incomeJar = incomeBudgetJars
                .Single(x => x.IncomeId == income.IncomeId && x.BudgetJarId == income.BudgetJarId);
                incomeJar.Amount = income.Amount;

                return true;
            });

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<IncomeBudgetJar, bool>>>(), It.IsAny<string[]?>()))
                .Returns(async (Expression<Func<IncomeBudgetJar, bool>> expression, string[]? includes) =>
                {
                    var incomeJar = incomeBudgetJars.FirstOrDefault(expression.Compile().Invoke);
                    if (incomeJar == null) throw new ArgumentException("Income BudgetJar not found.");
                    return await Task.FromResult(incomeJar);
                });

            return repo;
        }
    }
}
