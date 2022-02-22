using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Files
{
    public class FileUploadRequest
    {
        public Stream? Stream { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }

        public string Extension {
            get
            {
                if (string.IsNullOrWhiteSpace(Name)) return string.Empty;
                return Path.GetExtension(Name).ToLowerInvariant();
            }
        }

        public FileUploadRequest(Stream stream, string name, long size)
        {
            Stream = stream;
            Name = name;
            Size = size;
        }
    }
}
