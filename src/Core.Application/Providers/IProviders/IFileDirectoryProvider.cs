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
        string GetAttachmentDirectory(Guid userId);
        string GetProfileImageDirectory();
        string GetProfileImageThumbnailsDirectory();

    }
}
