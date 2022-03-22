using Core.Application.Services.IServices;
using MediatR;

namespace Core.Application.Mediator.Users
{
    public class UpdateUserPasswordRequest : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public UpdateUserPasswordRequest(Guid userId, string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }

    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordRequest, bool>
    {
        private readonly IUserService _userService;

        public UpdateUserPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _userService.UpdatePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);
        }
    }
}
