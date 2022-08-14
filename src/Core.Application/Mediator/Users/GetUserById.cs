using Core.Application.Models;
using Core.Application.Services.IServices;
using MediatR;

namespace Core.Application.Mediator.Users
{
    public class GetUserById : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
        public GetUserById(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = _userService.GetById(request.UserId);
            return await Task.FromResult(user);
        }
    }
}
