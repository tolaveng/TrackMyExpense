using Core.Application.Services.IServices;
using MediatR;

namespace Core.Application.Mediator.Users
{
    public class CheckUserPasswordRequest : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public CheckUserPasswordRequest(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }

    public class CheckUserPasswordHandler : IRequestHandler<CheckUserPasswordRequest, bool>
    {
        private readonly IUserService _userService;

        public CheckUserPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<bool> Handle(CheckUserPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _userService.CheckPasswordAsync(request.UserId, request.Password);
        }
    }
}
