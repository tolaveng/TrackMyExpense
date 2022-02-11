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
    public class SaveExpenseGroupCommand : IRequest<Guid>
    {
        public ExpenseGroupDto ExpenseGroupDto { get; set; }
        public SaveExpenseGroupCommand(ExpenseGroupDto expenseGroupDto)
        {
            ExpenseGroupDto =  expenseGroupDto;
        }
    }

    public class SaveExpenseGroupHandler : IRequestHandler<SaveExpenseGroupCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveExpenseGroupHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveExpenseGroupCommand request, CancellationToken cancellationToken)
        {
            var expenseGroup = _mapper.Map<ExpenseGroup>(request.ExpenseGroupDto);
            if (expenseGroup.Id == Guid.Empty)
            {
                expenseGroup.Id = Guid.NewGuid();
                expenseGroup.Icon = null;
                await _unitOfWork.ExpenseGroupRepository.InsertAsync(expenseGroup);
            }
            else
            {
                _unitOfWork.ExpenseGroupRepository.Update(expenseGroup);
            }

            await _unitOfWork.SaveAsync();

            return expenseGroup.Id;
        }
    }
}
