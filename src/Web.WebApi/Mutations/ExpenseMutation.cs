using Core.Application.Mediator.Expenses;
using Core.Application.Mediator.Users;
using Core.Application.Models;
using MediatR;

namespace Web.WebApi.Mutations
{
    public class ExpenseMutation
    {
        public async Task<ExpenseDto> SaveExpense([Service] IMediator mediator,
            [Service] IHttpContextAccessor httpContextAccessor,
            ExpenseDto expenseDto, List<AttachmentDto> attachments)
        {
            if (httpContextAccessor.HttpContext is null) {
                throw new ArgumentNullException("Cannot access HttpContext");
            }

            var user = await mediator.Send(new GetUserById(expenseDto.UserId));

            var expenseId = await mediator.Send(
                new SaveExpenseRequest(expenseDto, attachments));

            return await mediator.Send(new GetExpenseById(expenseId, expenseDto.UserId, user.TimeZone));
        }
    }
}
