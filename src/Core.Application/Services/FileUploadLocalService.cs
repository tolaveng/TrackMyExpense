using Core.Application.Common.Files;
using Core.Application.Providers.IProviders;
using Core.Application.Services.IServices;
using Core.Domain.Enums;

namespace Core.Application.Services
{
    public class FileUploadLocalService : IFileUploadService
    {
        public string[] AllowExtensions { get; set; } = { ".png", ".jpg", ".jpeg"};
        public long MaxFileSize { get; set; } = 100 * 1024; // 100 K

        public event EventHandler<int> UploadProgress;

        private readonly IFileDirectoryProvider FileDirectoryProvider;

        public FileUploadLocalService(IFileDirectoryProvider fileDirectoryProvider)
        {
            FileDirectoryProvider = fileDirectoryProvider;
        }

        public async Task<FileUploadResponse> UploadIconFileAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(file.Extension) || !AllowExtensions.Contains(file.Extension))
            {
                return FileUploadResponse.Fail($"File type must be ({string.Join(", ", AllowExtensions)})");
            }
            if (file.Size > MaxFileSize)
            {
                return FileUploadResponse.Fail($"File size must be less than {Math.Abs(MaxFileSize / 1024)} KB");
            }
            if (file.Size == 0 || file.Stream == null)
            {
                return FileUploadResponse.Fail("File is invalid");
            }

            if (string.IsNullOrEmpty(Path.GetExtension(saveFileName)))
            {
                saveFileName = $"{saveFileName}{file.Extension}";
            }
            
            var saveFilePath = Path.Combine(FileDirectoryProvider.GetIconDirectory(), $"{saveFileName}.uploading");
            try
            {
                await using FileStream writeStream = new(saveFilePath, FileMode.Create);
                using var readStream = file.Stream;
                var bytesRead = 0;
                var totalRead = 0;
                var buffer = new byte[1024 * 10];

                while ((bytesRead = await readStream.ReadAsync(buffer)) != 0)
                {
                    if (ct.IsCancellationRequested) break;

                    totalRead += bytesRead;

                    await writeStream.WriteAsync(buffer, 0, bytesRead);

                    var progress = Math.Ceiling(Decimal.Divide(totalRead, file.Size) * 100);
                    OnUploadProgress((int)progress);
                }
                writeStream.Close();
                if (ct.IsCancellationRequested) {
                    if (File.Exists(saveFilePath)) File.Delete(saveFilePath);
                    OnUploadProgress(0);
                    return FileUploadResponse.Fail("Fild upload have been cancelled");
                };

                File.Move(saveFilePath, saveFilePath.Replace(".uploading", ""), true);
                OnUploadProgress(100);
                return FileUploadResponse.Success(saveFileName);

            } catch (Exception ex)
            {
                OnUploadProgress(0);
                if (File.Exists(saveFilePath)) File.Delete(saveFilePath);
                return FileUploadResponse.Fail(ex.Message);
            }
        }

        
        protected virtual void OnUploadProgress(int progress)
        {
            UploadProgress?.Invoke(this, progress);
        }

        public async Task<bool> DeleteIconFileAsync(string filePath)
        {
            var deleteFile = Path.Combine(FileDirectoryProvider.GetIconDirectory(), filePath);
            if (!File.Exists(deleteFile)) return false;
            try
            {
                File.Delete(deleteFile);
                return await Task.FromResult(true);
            } catch (Exception)
            {
                // ignored
            }
            
            return false;
        }
    }
}
