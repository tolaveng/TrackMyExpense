using Core.Application.Providers.IProviders;
using Core.Application.Settings;
using Core.Domain.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Providers
{
    public class FileDirectoryProvider : IFileDirectoryProvider
    {
        private readonly FileUploadSetting FileUploadSetting;

        public FileDirectoryProvider(IOptions<FileUploadSetting> options)
        {
            FileUploadSetting = options.Value;
        }
        public string GetIconDirectory()
        {
            if (!Directory.Exists(FileUploadSetting.UploadDir))
            {
                Directory.CreateDirectory(FileUploadSetting.UploadDir);
            }

            var iconDir = Path.Combine(FileUploadSetting.UploadDir, "Icons");
            if (!Directory.Exists(iconDir))
            {
                Directory.CreateDirectory(iconDir);
            }

            return iconDir;
        }

        public string ResolveIconUrl(IconType iconType, string path)
        {
            if (string.IsNullOrEmpty(path)) return "";

            return iconType switch
            {
                IconType.Upload => $"{FileUploadSetting.UploadWebUrl}/icons/{path}",
                _ => path,
            };
        }
    }
}
