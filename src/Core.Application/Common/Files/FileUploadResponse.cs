using System;

namespace Core.Application.Common.Files
{
    public class FileUploadResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public static FileUploadResponse Success(string fileName)
        {
            return new FileUploadResponse() { Succeeded = true, FileName = fileName };
        }

        public static FileUploadResponse Success(string fileName, long fileSize)
        {
            return new FileUploadResponse() { Succeeded = true, FileName = fileName, FileSize = fileSize };
        }

        public static FileUploadResponse Fail(string message)
        {
            return new FileUploadResponse() { Succeeded = false, Message = message };
        }
    }
}
