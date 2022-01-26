using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class SysAttributeService : ISysAttributeService
    {
        private readonly IGenericRepository<SysAttribute> _sysAttributeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SysAttributeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sysAttributeRepo = unitOfWork.SysAttributeRepository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _sysAttributeRepo.DeleteAsync(id);
        }

        public async Task<SysAttributeDto> GetByIdAsync(Guid id)
        {
            var att = await _sysAttributeRepo.GetAsync(z => z.Id == id);
            if (att == null) return new SysAttributeDto();
            return _mapper.Map<SysAttributeDto>(att);
        }

        public async Task<SysAttributeDto> GetByNameAsync(string name)
        {
            var att = await _sysAttributeRepo.GetAsync(z => z.Name.ToLower() == name.ToLower());
            if (att == null) return new SysAttributeDto();
            return _mapper.Map<SysAttributeDto>(att);
        }

        public async Task<Guid> SaveAsync(SysAttributeDto sysAttributeDto)
        {
            var att = _mapper.Map<SysAttribute>(sysAttributeDto);
            
            if (att.Id == Guid.Empty)
            {
                att.Id = Guid.NewGuid();
                await _sysAttributeRepo.InsertAsync(att);
            }
            else
            {
                _sysAttributeRepo.Update(att);
            }
            await _unitOfWork.SaveAsync();
            return att.Id;
        }
    }
}
