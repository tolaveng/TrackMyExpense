using Core.Application.Services.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests
{
    public static class FileUploadFactoryMock
    {
        public static Mock<IFileUploadFactory> GetMock()
        {
            var mock = new Mock<IFileUploadFactory>();

            var fileUploadServiceMock = new Mock<IFileUploadService>();

            fileUploadServiceMock.Setup(x => x.DeleteAttachmentsAsync(ConstantMock.UserId, It.IsAny<string[]>()))
                .Returns(Task.FromResult(true));


            mock.Setup(x => x.GetFileUploadService()).Returns(() =>
            {
                return fileUploadServiceMock.Object;
            });

            return mock;
        }

    }
}
