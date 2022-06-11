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
    public static class BudgetJarRepositoryMock
    {
        public static Mock<IGenericRepository<BudgetJar>> GetRepository()
        {
            var budgetJars = new List<BudgetJar>();

            var icon = new Icon()
            {
                Id = ConstantMock.IconId,
                Name = "Icon1",
                Path = ""
            };
            // Total balances comes from income mock
            budgetJars.Add(new BudgetJar()
            {
                UserId = ConstantMock.UserId,
                Id = ConstantMock.BudgetJarId1,
                Name = "BudgetJar1",
                Icon = icon,
                IconId = icon.Id,
                TotalBalance = 140,
                Percentage = 70,
            });

            budgetJars.Add(new BudgetJar()
            {
                UserId = ConstantMock.UserId,
                Id = ConstantMock.BudgetJarId2,
                Name = "BudgetJar2",
                Icon = icon,
                IconId = icon.Id,
                TotalBalance = 60,
                Percentage = 30,
            });


            var repo = new Mock<IGenericRepository<BudgetJar>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(budgetJars);

            repo.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<BudgetJar, bool>>>(),
                It.IsAny<Func<IQueryable<BudgetJar>, IOrderedQueryable<BudgetJar>>?>(),
                It.IsAny<string[]?>()))
                .Returns((Expression<Func<BudgetJar, bool>> expression,
                Func<IQueryable<BudgetJar>, IOrderedQueryable<BudgetJar>> ? orderBy,
                string[]? includes) =>
                {
                    return Task.FromResult(budgetJars.Where(expression.Compile().Invoke));
                });

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BudgetJar, bool>>>(), It.IsAny<string[]?>()))
                .Returns(async (Expression<Func<BudgetJar, bool>> expression, string[]? includes) =>
                {
                    var jar = budgetJars.FirstOrDefault(expression.Compile().Invoke);
                    if (jar == null) throw new ArgumentException("Budget jar not found.");
                    return await Task.FromResult(jar);
                });


            repo.Setup(x => x.Update(It.IsAny<BudgetJar>())).Returns((BudgetJar budgetJar) =>
            {
                var jar = budgetJars.Single(x => x.Id == budgetJar.Id);
                jar.Name = budgetJar.Name;
                jar.TotalBalance = budgetJar.TotalBalance;

                return true;
            });

            repo.Setup(x => x.InsertAsync(It.IsAny<BudgetJar>())).Returns((BudgetJar budgetJar) =>
            {
                return Task.FromResult(true);
            });


            return repo;
        }
    }
}
