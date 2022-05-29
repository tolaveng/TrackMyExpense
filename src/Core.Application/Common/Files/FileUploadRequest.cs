namespace Core.Application.Common.Files
{
    public class FileUploadRequest
    {
        public Stream? Stream { get; set; }
        public string FileName { get; set; } = string.Empty;

        public string[] ImageExtensions => new[] { ".png", ".jpg", ".jpeg", ".gif"};
        public string Extension {
            get
            {
                if (string.IsNullOrWhiteSpace(FileName)) return string.Empty;
                return Path.GetExtension(FileName).ToLowerInvariant();
            }
        }

        public bool IsImage
        {
            get
            {
                return ImageExtensions.Contains(Extension);
            }
        }

        public FileUploadRequest(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }
    }
}
