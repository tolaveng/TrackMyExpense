using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Mapper;
using Core.Application.Mediator.Expenses;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using Core.Tests.Repositories;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Expenses
{
    public class SaveExpenseTests
    {

        private readonly IMapper _mapper;
        private readonly SaveExpenseRequestHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IFileUploadFactory> _fileUploadFactory;
        

        public SaveExpenseTests()
        {
            _fileUploadFactory = FileUploadFactoryMock.GetMock();
            _unitOfWorkMock = UnitOfWorkMock.GetMock();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _handler = new SaveExpenseRequestHandler(_unitOfWorkMock.Object, _mapper, _fileUploadFactory.Object);
        }


        [Fact]
        public async void SaveExpense_Should_Save_New_One()
        {
            var expense = new ExpenseDto()
            {
                Id = Guid.Empty,
                BudgetJarId = Guid.Parse("BCA42767-831C-43A4-A3AC-9ECBC74A223F"), // see budgetjar mock
                
                Attachments = new List<AttachmentDto>(),
                Amount = 50,
                Description = "Test",
                PaidDate = new DateTime(2022, 5, 1),
                PaymentMethod = Domain.Enums.PaymentMethod.Cash
            };

            var request = new SaveExpenseRequest(expense, new List<AttachmentDto>());
            var guid = await _handler.Handle(request, CancellationToken.None);
            var budgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.Equal(50, budgetJar.TotalBalance);
        }

        [Fact]
        public async void SaveExpense_Should_Save_Existing()
        {
            var expense = await _unitOfWorkMock.Object.ExpenseRepository
                .GetAsync(x => x.Id == Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274"));
            if (expense == null) throw new ArgumentNullException("Expense not found");

            var attachments = await _unitOfWorkMock.Object.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id);
            var attachmentsDto = _mapper.Map<List<AttachmentDto>>(attachments);
            
            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            expenseDto.ExpenseGroupId = Guid.Parse("12B8589B-6647-43C8-859C-0AD6BFD8F967");

            var request = new SaveExpenseRequest(expenseDto, attachmentsDto);
            var guid = await _handler.Handle(request, CancellationToken.None);
            
            var budgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);
            var updatedExpense = await _unitOfWorkMock.Object.ExpenseRepository.GetAsync(x => x.Id == expense.Id);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.NotNull(updatedExpense);
            Assert.Equal(updatedExpense.ExpenseGroupId, Guid.Parse("12B8589B-6647-43C8-859C-0AD6BFD8F967"));
            Assert.Equal(100, budgetJar.TotalBalance);
        }

        [Fact]
        public async void SaveExpense_Should_Save_Existing_Update_Amount()
        {
            var expense = await _unitOfWorkMock.Object.ExpenseRepository
                .GetAsync(x => x.Id == Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274"));
            if (expense == null) throw new ArgumentNullException("Expense not found");

            var attachments = await _unitOfWorkMock.Object.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id);
            var attachmentsDto = _mapper.Map<List<AttachmentDto>>(attachments);

            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            expenseDto.Amount = 50;

            var request = new SaveExpenseRequest(expenseDto, attachmentsDto);
            var guid = await _handler.Handle(request, CancellationToken.None);

            var budgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);
            var updatedExpense = await _unitOfWorkMock.Object.ExpenseRepository.GetAsync(x => x.Id == expense.Id);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.NotNull(updatedExpense);
            Assert.Equal(50, updatedExpense.Amount);
            Assert.Equal(75, budgetJar.TotalBalance); // 100 + 25 - 50
        }

        [Fact]
        public async void SaveExpense_Should_Save_Existing_Update_BudgetJar()
        {
            var expense = await _unitOfWorkMock.Object.ExpenseRepository
                .GetAsync(x => x.Id == Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274"));
            if (expense == null) throw new ArgumentNullException("Expense not found");

            var attachments = await _unitOfWorkMock.Object.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id);
            var attachmentsDto = _mapper.Map<List<AttachmentDto>>(attachments);

            var oldBudgetJarId = expense.BudgetJarId;
            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            expenseDto.BudgetJarId = Guid.Parse("C7F0319A-719F-4A0D-872B-96E4FD2CC6F2");

            var request = new SaveExpenseRequest(expenseDto, attachmentsDto);
            var guid = await _handler.Handle(request, CancellationToken.None);

            var oldBudgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == oldBudgetJarId);
            var newBudgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == expenseDto.BudgetJarId);
            var updatedExpense = await _unitOfWorkMock.Object.ExpenseRepository.GetAsync(x => x.Id == expenseDto.Id);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.NotNull(updatedExpense);
            Assert.Equal(125, oldBudgetJar.TotalBalance); // 100 + 25
            Assert.Equal(25, newBudgetJar.TotalBalance); // 50 - 25
        }

        [Fact]
        public async void SaveExpense_Should_Save_Existing_Update_BudgetJar_Amount()
        {
            var expense = await _unitOfWorkMock.Object.ExpenseRepository
                .GetAsync(x => x.Id == Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274"));
            if (expense == null) throw new ArgumentNullException("Expense not found");

            var attachments = await _unitOfWorkMock.Object.AttachmentRepository.GetAllAsync(x => x.ExpenseId == expense.Id);
            var attachmentsDto = _mapper.Map<List<AttachmentDto>>(attachments);

            var oldBudgetJarId = expense.BudgetJarId;
            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            expenseDto.BudgetJarId = Guid.Parse("C7F0319A-719F-4A0D-872B-96E4FD2CC6F2");
            expenseDto.Amount = 30;

            var request = new SaveExpenseRequest(expenseDto, attachmentsDto);
            var guid = await _handler.Handle(request, CancellationToken.None);

            var oldBudgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == oldBudgetJarId);
            var newBudgetJar = await _unitOfWorkMock.Object.BudgetJarRepository.GetAsync(x => x.Id == expenseDto.BudgetJarId);
            var updatedExpense = await _unitOfWorkMock.Object.ExpenseRepository.GetAsync(x => x.Id == expenseDto.Id);

            Assert.NotEqual(Guid.Empty, guid);
            Assert.NotNull(updatedExpense);
            Assert.Equal(125, oldBudgetJar.TotalBalance); // 100 + 25
            Assert.Equal(20, newBudgetJar.TotalBalance); // 50 - 30
        }
    }
}
