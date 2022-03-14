using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Users
{
    public class UpdateProfileImageRequest : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string ImageName { get; set; }

        public UpdateProfileImageRequest(Guid userId, string imageName)
        {
            UserId = userId;
            ImageName = imageName;
        }
    }

    public class UpdateProfileImageHandler : IRequestHandler<UpdateProfileImageRequest, bool>
    {
        public IUserRepository _userRepo;
        public UpdateProfileImageHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UpdateProfileImageRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepo.GetById(request.UserId);
            if (user == null) return false;

            user.ProfileImage = request.ImageName;
            var result = await _userRepo.UpdateUserAsync(user);

            return result.Succeeded;
        }
    }
}
