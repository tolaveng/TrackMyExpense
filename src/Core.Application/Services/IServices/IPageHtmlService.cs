using Core.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.IServices
{
    public interface IPageHtmlService
    {
        Task<PageHtmlDto> GetByIdAsync(Guid id);
        Task<PageHtmlDto> GetByNameAsync(string name);
        Task<Guid> SaveAsync(PageHtmlDto pageHtmlDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
