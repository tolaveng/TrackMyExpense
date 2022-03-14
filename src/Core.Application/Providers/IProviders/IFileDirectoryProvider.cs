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
        string GetUploadDirectory(string[] subDirectories);
        string GetIconDirectory();
        string GetProfileImageDirectory();
        string GetProfileImageThumbnailsDirectory();
        string ResolveDirectoryUrl(string[] subDirectories, string baseUri);
        string ResolveIconUrl(IconType iconType, string path);
        string ResolveProfileImageUrl(string fileName, string baseUri);
        string ResolveProfileImageThumbnailUrl(string fileName, string baseUri);
    }
}
