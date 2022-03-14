namespace Core.Application.Common.Files
{
    public class FileUploadRequest
    {
        public Stream? Stream { get; set; }
        public string FileName { get; set; } = string.Empty;

        public string Extension {
            get
            {
                if (string.IsNullOrWhiteSpace(FileName)) return string.Empty;
                return Path.GetExtension(FileName).ToLowerInvariant();
            }
        }

        public FileUploadRequest(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }
    }
}
