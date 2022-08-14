using Core.Application.Mediator.Expenses;
using Core.Application.Models;
using MediatR;

namespace Web.WebApi.Queries
{
    [ExtendObjectType("Query")]
    public class ExpenseQuery
    {
        public async Task<ExpenseDto> GetExpense([Service] IMediator mediator,
            Guid userId, Guid expenseId, string timeZoneId)
        {
            return await mediator.Send(new GetExpenseById(userId, expenseId, timeZoneId));
        }
    }
}
