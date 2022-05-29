using Core.Application.Common.Files;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.IServices
{
    public interface IFileUploadService
    {
        string[] AllowExtensions { get; set; }
        long MaxFileSize { get; set; }

        public event EventHandler<int> UploadProgress;

        Task<FileUploadResponse> SaveIconFileAsync(FileUploadRequest fileUploadRequest,
            string saveFileName, CancellationToken ct);
        Task<FileUploadResponse> SaveProfileImageAsync(FileUploadRequest fileUploadRequest,
            string saveFileName, CancellationToken ct);

        Task<FileUploadResponse> SaveAttachmentAsync(FileUploadRequest fileUploadRequest,
            string saveFileName, CancellationToken ct);

        Task<FileUploadResponse> SaveProfileImageThumbnailAsync(FileUploadRequest fileUploadRequest,
            string saveFileName, CancellationToken ct);

        Task<bool> DeleteIconFileAsync(string filePath);
        Task<bool> DeleteProfileImageAsync(string imageName);
        Task<bool> DeleteProfileImageThumbnailAsync(string imageName);

        Task<bool> DeleteAttachmentsAsync(string[] fileNames);
    }
}
