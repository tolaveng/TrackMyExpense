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
                Id = Guid.Parse("CFC2D349-BBAA-4228-8B0C-8CF91714070B"),
                Name = "Icon1",
                Path = ""
            };

            budgetJars.Add(new BudgetJar()
            {
                Id = Guid.Parse("BCA42767-831C-43A4-A3AC-9ECBC74A223F"),
                Name = "BudgetJar1",
                Icon = icon,
                IconId = icon.Id,
                TotalBalance = 100
            });

            budgetJars.Add(new BudgetJar()
            {
                Id = Guid.Parse("C7F0319A-719F-4A0D-872B-96E4FD2CC6F2"),
                Name = "BudgetJar2",
                Icon = icon,
                IconId = icon.Id,
                TotalBalance = 50
            });


            var repo = new Mock<IGenericRepository<BudgetJar>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(budgetJars);

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BudgetJar, bool>>>(), It.IsAny<string[]?>()))
                .Returns( async (Expression<Func<BudgetJar, bool>> expression, string[]? includes) =>
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
