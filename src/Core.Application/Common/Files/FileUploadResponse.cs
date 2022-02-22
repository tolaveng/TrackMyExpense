using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Files
{
    public class FileUploadResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;

        public static FileUploadResponse Success(string fileName)
        {
            return new FileUploadResponse() { Succeeded = true, FileName = fileName };
        }

        public static FileUploadResponse Fail(string message)
        {
            return new FileUploadResponse() { Succeeded = false, Message = message };
        }
    }
}
