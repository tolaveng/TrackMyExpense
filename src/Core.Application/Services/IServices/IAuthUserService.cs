using Core.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.IServices
{
    public interface IAuthUserService
    {
        Task<UserDto?> GetAuthUserAsync();
    }
}
