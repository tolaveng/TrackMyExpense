using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class PageHtmlService : IPageHtmlService
    {
        private readonly IGenericRepository<PageHtml> _pageHtmlRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PageHtmlService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pageHtmlRepo = unitOfWork.PageHtmlRepository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _pageHtmlRepo.DeleteAsync(id);
        }

        public async Task<PageHtmlDto> GetByIdAsync(Guid id)
        {
            var page = await _pageHtmlRepo.GetAsync(z => z.Id == id);
            if (page == null) return new PageHtmlDto();
            return _mapper.Map<PageHtmlDto>(page);
        }

        public async Task<PageHtmlDto> GetByNameAsync(string name)
        {
            var page = await _pageHtmlRepo.GetAsync(z => z.Name.ToLower() == name.ToLower());
            if (page == null) return new PageHtmlDto();
            return _mapper.Map<PageHtmlDto>(page);
        }

        public async Task<Guid> SaveAsync(PageHtmlDto pageHtmlDto)
        {
            var page = _mapper.Map<PageHtml>(pageHtmlDto);

            if (page.Id == Guid.Empty)
            {
                page.Id = Guid.NewGuid();
                await _pageHtmlRepo.InsertAsync(page);
            }
            else
            {
                _pageHtmlRepo.Update(page);
            }
            await _unitOfWork.SaveAsync();
            return page.Id;
        }
    }
}
