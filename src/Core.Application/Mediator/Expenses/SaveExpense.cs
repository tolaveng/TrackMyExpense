using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Expenses
{
    public class SaveExpenseRequest : IRequest<Guid>
    {
        public SaveExpenseRequest(ExpenseDto expense, List<AttachmentDto> attachments)
        {
            Expense = expense;
            Attachments = attachments;
        }

        public ExpenseDto Expense { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
    }

    public class SaveExpenseRequestHandler : IRequestHandler<SaveExpenseRequest, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUploadFactory _fileUploadFactory;

        public SaveExpenseRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadFactory FileUploadFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploadFactory = FileUploadFactory;
        }

        public async Task<Guid> Handle(SaveExpenseRequest request, CancellationToken cancellationToken)
        {
            var dbBudgetJarId = Guid.Empty;
            var dbAmount = 0m;
            var dbAttachments = Array.Empty<Attachment>();
            var expense = _mapper.Map<Expense>(request.Expense);

            // prevent recreation
            expense.Attachments = null;
            expense.BudgetJar = null;
            expense.Category = null;

            if (expense.Id != Guid.Empty)
            {
                var dbExpense = await _unitOfWork.ExpenseRepository.GetAsync(x => x.Id == expense.Id);
                if (dbExpense == null)
                {
                    throw new InvalidOperationException("Expense is not found");
                }
                dbBudgetJarId = dbExpense.BudgetJarId;
                dbAmount = dbExpense.Amount;
                dbAttachments = (await _unitOfWork.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id)).ToArray();
                _unitOfWork.ExpenseRepository.Update(expense);
            }
            else
            {
                expense.Id = Guid.NewGuid();
                var result = await _unitOfWork.ExpenseRepository.InsertAsync(expense);
                if (!result) return Guid.Empty;
            }

            // Attachments, We don't update attachment, only delete and insert
            var attachments = _mapper.Map<List<Attachment>>(request.Attachments);
            var deleteAttachments = dbAttachments.Where(x => attachments.All(y => y.Id != x.Id));
            if (deleteAttachments.Any())
            {
                var fileUploadService = _fileUploadFactory.GetFileUploadService();
                await fileUploadService.DeleteAttachmentsAsync(deleteAttachments.Select(x => x.FileName).ToArray());
                _unitOfWork.AttachmentRepository.DeleteRange(deleteAttachments);
            }

            var insertAttachments = attachments.Where(x => dbAttachments.All(y => y.Id != x.Id));
            if (insertAttachments.Any())
            {
                await _unitOfWork.AttachmentRepository.InsertRangeAsync(insertAttachments);
            }
            
            // budget jar has changed
            if (dbBudgetJarId != Guid.Empty && dbBudgetJarId != request.Expense.BudgetJarId)
            {
                var prevBudgetJar = await _unitOfWork.BudgetJarRepository.GetAsync(x => x.Id == dbBudgetJarId);
                prevBudgetJar.TotalBalance = prevBudgetJar.TotalBalance + dbAmount;
                _unitOfWork.BudgetJarRepository.Update(prevBudgetJar);

                var budgetJar = await _unitOfWork.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);
                budgetJar.TotalBalance = budgetJar.TotalBalance - expense.Amount;
                _unitOfWork.BudgetJarRepository.Update(budgetJar);
            }
            else
            {
                // Update Balance if the amount has changed
                if (dbAmount != expense.Amount)
                {
                    var budgetJar = await _unitOfWork.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);
                    budgetJar.TotalBalance = budgetJar.TotalBalance + dbAmount - expense.Amount;
                    _unitOfWork.BudgetJarRepository.Update(budgetJar);
                }
            }
            
            await _unitOfWork.SaveAsync();
            return expense.Id;
        }
    }
}
