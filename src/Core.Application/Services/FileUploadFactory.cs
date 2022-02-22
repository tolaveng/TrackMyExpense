using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class FileUploadFactory : IFileUploadFactory
    {
        private readonly IHostingEnvironment Environment;
        private readonly IServiceProvider ServiceProvider;

        public FileUploadFactory(IHostingEnvironment environment, IServiceProvider serviceProvider)
        {
            Environment = environment;
            ServiceProvider = serviceProvider;
        }


        public IFileUploadService GetFileUploadService()
        {
            if (Environment.IsDevelopment())
            {
                return (IFileUploadService)ServiceProvider.GetService(typeof(FileUploadLocalService));

            }

            //return (IFileUploadService)ServiceProvider.GetService(typeof(FileUploadAzureService));
            throw new NotImplementedException();
        }
    }
}

// Ref: https://medium.com/null-exception/factory-pattern-using-built-in-dependency-injection-of-asp-net-core-f91bd3b58665#:~:text=3%20min%20read-,Factory%20pattern%20using%20built%2Din%20dependency%20injection%20of%20ASP.Net,creation%20logic%20to%20the%20client.
