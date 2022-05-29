using Core.Application.Providers.IProviders;
using Core.Application.Settings;
using Core.Domain.Enums;
using Microsoft.Extensions.Options;


namespace Core.Application.Providers
{
    public class FileDirectoryProvider : IFileDirectoryProvider
    {
        private readonly FileUploadSetting FileUploadSetting;

        public FileDirectoryProvider(IOptions<FileUploadSetting> options)
        {
            FileUploadSetting = options.Value;
        }

        public string GetAssetFileUrl(string fileName, string baseUri)
        {
            return $"{baseUri}/assets/{fileName}";
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

        public string GetUploadDirectoryUrl(string[] subDirectories, string baseUri)
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

        public string GetIconUrl(IconType iconType, string path)
        {
            if (string.IsNullOrEmpty(path)) return "";

            return iconType switch
            {
                IconType.Upload => $"{GetUploadDirectoryUrl(new[] { "icons" }, string.Empty)}{path}",
                _ => path,
            };
        }

        public string GetProfileImageUrl(string fileName, string baseUri)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{GetUploadDirectoryUrl(new[] { "profileimages" }, baseUri)}{fileName}";
        }

        public string GetProfileImageThumbnailUrl(string fileName, string baseUri)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{GetUploadDirectoryUrl(new[] { "profileimages", "thumbnails" }, baseUri)}{fileName}";
        }

        public string GetAttachmentDirectory()
        {
            return GetUploadDirectory(new string[] { "Attachments" });
        }

        public string GetAttachmentUrl(string fileName, string baseUri)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{GetUploadDirectoryUrl(new[] { "attachments" }, baseUri)}{fileName}";
        }
    }
}
