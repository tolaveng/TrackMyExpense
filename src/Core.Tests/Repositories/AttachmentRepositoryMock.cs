using Core.Application.IRepositories;
using Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Tests.Repositories
{
    public static class AttachmentRepositoryMock
    {
        public static Mock<IGenericRepository<Attachment>> GetRepository()
        {
            var attachments = new List<Attachment>();

            attachments.Add(new Attachment()
            {
                Id = Guid.Parse("CEF99BD2-4640-4CAF-BCEA-F7AEDE1A3051"),
                ExpenseId = Guid.Empty,
                Name = "Test Attachment default",
                FileName = "Test_Attachment_default",
            });

            attachments.Add(new Attachment()
            {
                Id = Guid.Parse("19BDE9C9-2412-439A-B691-F5677A8738E6"),
                ExpenseId = Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274"), // see Expense repo mock
                Name = "Test Attachment Expense",
                FileName = "Test_Attachment_Expense",
            });

            var repo = new Mock<IGenericRepository<Attachment>>();

            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(attachments);

            repo.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Attachment, bool>>?>(),
                It.IsAny<Func<IQueryable<Attachment>, IOrderedQueryable<Attachment>>?>(),
                It.IsAny<string[]?>()
                )
            )
                .Returns(async (Expression<Func<Attachment, bool>> expression,
                    Func<IQueryable<Attachment>, IOrderedQueryable<Attachment>> orderBy,
                    string[]? includes
                ) =>
                {
                    var atts = attachments.Where(expression.Compile().Invoke);
                    return await Task.FromResult(atts);
                });

            repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Attachment, bool>>>(), It.IsAny<string[]?>()))
                .Returns(async (Expression<Func<Attachment, bool>> expression, string[]? includes) =>
                {
                    var att = attachments.FirstOrDefault(expression.Compile().Invoke);
                    if (att == null) throw new ArgumentException("Attachment not found.");
                    return await Task.FromResult(att);
                });

            return repo;
        }
    }
}
