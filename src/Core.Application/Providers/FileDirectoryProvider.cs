using Core.Application.Providers.IProviders;
using Core.Application.Settings;
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

        public string GetAttachmentDirectory(Guid userId)
        {
            return GetUploadDirectory(new string[] { "Attachments", userId.ToString() });
        }
    }
}
