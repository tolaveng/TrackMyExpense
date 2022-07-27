using Core.Application.Services.IServices;
using Core.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Application.Services
{
    public class FileUploadFactory : IFileUploadFactory
    {
        public readonly AzureStorageSetting AzureSetting;
        private readonly IServiceProvider ServiceProvider;

        public FileUploadFactory(IOptions<AzureStorageSetting> options, IServiceProvider serviceProvider)
        {
            AzureSetting = options.Value;
            ServiceProvider = serviceProvider;
        }


        public IFileUploadService GetFileUploadService()
        {
            if (AzureSetting.IsEnabled)
            {
                return (IFileUploadService)ServiceProvider.GetService(typeof(AzureFileUploadService));
            }

            return (IFileUploadService)ServiceProvider.GetService(typeof(LocalFileUploadService));
        }
    }
}

// Ref: https://medium.com/null-exception/factory-pattern-using-built-in-dependency-injection-of-asp-net-core-f91bd3b58665#:~:text=3%20min%20read-,Factory%20pattern%20using%20built%2Din%20dependency%20injection%20of%20ASP.Net,creation%20logic%20to%20the%20client
