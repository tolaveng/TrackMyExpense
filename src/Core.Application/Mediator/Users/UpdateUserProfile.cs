using Core.Application.Common;
using Core.Application.Models;
using Core.Application.Services.IServices;
using MediatR;

namespace Core.Application.Mediator.Users
{
    public class UpdateUserProfileRequest : IRequest<GenericResponse<bool>>
    {
        public UserDto User { get; set; }

        public UpdateUserProfileRequest(UserDto user)
        {
            User = user;
        }
    }

    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileRequest, GenericResponse<bool>>
    {
        private readonly IUserService _userService;

        public UpdateUserProfileHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<GenericResponse<bool>> Handle(UpdateUserProfileRequest request, CancellationToken cancellationToken)
        {
            var user = request.User;
            return await _userService.UpdateUserAsync(user);
        }
    }
}
