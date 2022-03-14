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

        public string GetUploadDirectory(string[] subDirectories)
        {
            var dirPath = FileUploadSetting.UploadDir;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            foreach(var subDirectory in subDirectories)
            {
                dirPath = Path.Combine(dirPath, subDirectory);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
            
            return dirPath;
        }

        public string GetIconDirectory()
        {
            return GetUploadDirectory(new string[] { "Icons" });
        }

        public string GetProfileImageDirectory()
        {
            return GetUploadDirectory(new string[] { "ProfileImages" });
        }

        public string GetProfileImageThumbnailsDirectory()
        {
            return GetUploadDirectory(new string[] { "ProfileImages", "Thumbnails" });
        }

        public string ResolveDirectoryUrl(string[] subDirectories, string baseUri)
        {
            var url = FileUploadSetting.UploadWebUrl;
            if (!string.IsNullOrEmpty(baseUri))
            {
                url = $"{baseUri.TrimEnd('/')}{url}";
            }
            foreach (var subDirectory in subDirectories)
            {
                if (string.IsNullOrEmpty(subDirectory)) continue;
                url = $"{url}/{subDirectory}";
            }

            return $"{url}/";
        }

        public string ResolveIconUrl(IconType iconType, string path)
        {
            if (string.IsNullOrEmpty(path)) return "";

            return iconType switch
            {
                IconType.Upload => $"{ResolveDirectoryUrl(new[] { "icons" }, string.Empty)}{path}",
                _ => path,
            };
        }

        public string ResolveProfileImageUrl(string fileName, string baseUri)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{ResolveDirectoryUrl(new[] { "profileimages" }, baseUri)}{fileName}";
        }
        public string ResolveProfileImageThumbnailUrl(string fileName, string baseUri)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{ResolveDirectoryUrl(new[] { "profileimages", "thumbnails" }, baseUri)}{fileName}";
        }
    }
}
