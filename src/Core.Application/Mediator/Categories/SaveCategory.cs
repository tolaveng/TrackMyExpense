using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Categories
{
    public class SaveCategoryCommand : IRequest<Guid>
    {
        public CategoryDto CategoryDto { get; set; }
        public SaveCategoryCommand(CategoryDto categoryDto)
        {
            CategoryDto =  categoryDto;
        }
    }

    public class SaveCategoryHandler : IRequestHandler<SaveCategoryCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.CategoryDto);
            if (category.Id == Guid.Empty)
            {
                category.Id = Guid.NewGuid();
                category.Icon = null;
                await _unitOfWork.CategoryRepository.InsertAsync(category);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(category);
            }

            await _unitOfWork.SaveAsync();

            return category.Id;
        }
    }
}
