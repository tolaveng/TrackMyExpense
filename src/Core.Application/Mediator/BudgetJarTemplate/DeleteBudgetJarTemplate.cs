﻿using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.BudgetJars
{
    public class DeleteBudgetJarTemplateCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public DeleteBudgetJarTemplateCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteBudgetJarTemplateHandler : IRequestHandler<DeleteBudgetJarTemplateCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteBudgetJarTemplateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteBudgetJarTemplateCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarTemplateRepository;
            var deleted = await repo.DeleteAsync(request.Id);
            if (deleted)
            {
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
