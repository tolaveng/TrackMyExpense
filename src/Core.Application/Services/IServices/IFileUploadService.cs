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
        Task<FileUploadResponse> UploadIconFileAsync(FileUploadRequest fileUploadRequest,
            string saveFileName, CancellationToken ct);

        Task<bool> DeleteIconFileAsync(string filePath);
    }
}
