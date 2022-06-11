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
    public static class ExpenseRepositoryMock
    {
        public static Mock<IGenericRepository<Expense>> GetRepository()
        {
            var expenses = new List<Expense>();
            expenses.Add(new Expense() {
                Id = ConstantMock.ExpenseId,
                Amount = 25,
                Description = "Test expense",
                BudgetJarId = ConstantMock.BudgetJarId1
            });

            var repo = new Mock<IGenericRepository<Expense>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(expenses);

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Expense, bool>>>(), It.IsAny<string[]?>()))
                .Returns(async (Expression<Func<Expense, bool>> expression, string[]? includes) =>
                {
                    var exp = expenses.FirstOrDefault(expression.Compile().Invoke);
                    if (exp == null) throw new ArgumentException("Expense not found.");
                    return await Task.FromResult(exp);
                });

            repo.Setup(x => x.Update(It.IsAny<Expense>())).Returns((Expense expense) =>
            {
                var exp = expenses.Single(x => x.Id == expense.Id);
                exp.Attachments = expense.Attachments;
                exp.ExpenseGroup = expense.ExpenseGroup;
                exp.ExpenseGroupId = expense.ExpenseGroupId;
                exp.Amount = expense.Amount;
                exp.BudgetJar = expense.BudgetJar;
                exp.BudgetJarId = expense.BudgetJarId;
                exp.Description = expense.Description;
                exp.UserId = expense.UserId;
                exp.ModifiedAt = expense.ModifiedAt;
                

                return true;
            });

            repo.Setup(x => x.InsertAsync(It.IsAny<Expense>())).Returns((Expense expense) =>
            {
                expenses.Add(expense);
                return Task.FromResult(true);
            });


            return repo;
        }

    }
}
