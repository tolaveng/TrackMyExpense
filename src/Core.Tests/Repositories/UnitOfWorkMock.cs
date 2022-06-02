using Core.Application.IRepositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Repositories
{
    public static class UnitOfWorkMock
    {
        public static Mock<IUnitOfWork> GetMock()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            mock.Setup(x => x.ExpenseRepository).Returns(ExpenseRepositoryMock.GetRepository().Object);
            mock.Setup(x => x.BudgetJarRepository).Returns(BudgetJarRepositoryMock.GetRepository().Object);
            mock.Setup(x => x.AttachmentRepository).Returns(AttachmentRepositoryMock.GetRepository().Object);

            return mock;
        }
    }
}
