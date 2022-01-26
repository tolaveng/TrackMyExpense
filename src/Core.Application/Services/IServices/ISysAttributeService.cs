using Core.Application.Models;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.IServices
{
    public interface ISysAttributeService
    {
        Task<SysAttributeDto> GetByIdAsync(Guid id);
        Task<SysAttributeDto> GetByNameAsync(string name);
        Task<Guid> SaveAsync(SysAttributeDto sysAttribute);
        Task<bool> DeleteAsync(Guid id);
    }
}
