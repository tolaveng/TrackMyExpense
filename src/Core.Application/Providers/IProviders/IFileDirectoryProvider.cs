using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Providers.IProviders
{
    public interface IFileDirectoryProvider
    {
        string GetAssetFileUrl(string fileName, string baseUri);
        string GetUploadDirectory(string[] subDirectories);
        string GetIconDirectory();
        string GetAttachmentDirectory(Guid userId);
        string GetProfileImageDirectory();
        string GetProfileImageThumbnailsDirectory();
        string GetUploadDirectoryUrl(string[] subDirectories, string baseUri);
        string GetIconUrl(IconType iconType, string path);
        string GetProfileImageUrl(string fileName, string baseUri);
        string GetAttachmentUrl(Guid userId, string fileName, string baseUri);
        string GetProfileImageThumbnailUrl(string fileName, string baseUri);

        bool CheckUserAttachmentUrl(string userId, string url);
    }
}
