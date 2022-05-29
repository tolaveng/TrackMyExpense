using Core.Application.Common.Files;
using Core.Application.Utils;
using Core.Application.Providers.IProviders;
using Core.Application.Services.IServices;

namespace Core.Application.Services
{
    public class FileUploadLocalService : IFileUploadService
    {
        public string[] AllowExtensions { get; set; } = { ".png", ".jpg", ".jpeg"};
        public long MaxFileSize { get; set; } = 100 * 1024; // 100 K

        public event EventHandler<int>? UploadProgress;

        private readonly IFileDirectoryProvider FileDirectoryProvider;

        public FileUploadLocalService(IFileDirectoryProvider fileDirectoryProvider)
        {
            FileDirectoryProvider = fileDirectoryProvider;
        }

        public async Task<FileUploadResponse> SaveIconFileAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            return await SaveFileAsync(file, FileDirectoryProvider.GetIconDirectory(), saveFileName, ct);
        }

        public async Task<FileUploadResponse> SaveProfileImageAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var profileImageDir = FileDirectoryProvider.GetProfileImageDirectory();
            var result = await SaveFileAsync(file, profileImageDir, saveFileName, ct);
            if (result.Succeeded)
            {
                ImageSharpHelper.ResizeAndSave(Path.Combine(profileImageDir, saveFileName));
            }
            return result;
        }

        public async Task<FileUploadResponse> SaveAttachmentAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var imageDir = FileDirectoryProvider.GetAttachmentDirectory();
            var result = await SaveFileAsync(file, imageDir, saveFileName, ct);
            if (result.Succeeded && file.IsImage)
            {
                result.FileSize = ImageSharpHelper.ResizeAndSave(Path.Combine(imageDir, result.FileName));
            }
            return result;
        }

        public async Task<FileUploadResponse> SaveProfileImageThumbnailAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            return await SaveFileAsync(file, FileDirectoryProvider.GetProfileImageThumbnailsDirectory(), saveFileName, ct);
        }

        /// <summary>
        /// Save file to disk in directory path, invoke upload progress percentage
        /// </summary>
        /// <param name="file">File request stream</param>
        /// <param name="saveDirectoryPath">Path to save the file</param>
        /// <param name="saveFileName">File name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>File Upload Response</returns>
        public async Task<FileUploadResponse> SaveFileAsync(FileUploadRequest file,
            string saveDirectoryPath,
            string saveFileName,
            CancellationToken ct)
        {
            if (file.Stream == null || file.Stream.Length == 0)
            {
                return FileUploadResponse.Fail("File is invalid");
            }
            if (string.IsNullOrEmpty(file.Extension) || !AllowExtensions.Contains(file.Extension))
            {
                return FileUploadResponse.Fail($"File type must be ({string.Join(", ", AllowExtensions)})");
            }
            if (file.Stream.Length > MaxFileSize)
            {
                return FileUploadResponse.Fail($"File size must be less than {Math.Abs(MaxFileSize / 1024)} KB");
            }
            
            // Append an extension, when saving without it.
            if (string.IsNullOrEmpty(Path.GetExtension(saveFileName)))
            {
                saveFileName = $"{saveFileName}{file.Extension}";
            }

            var saveFilePath = Path.Combine(saveDirectoryPath, $"{saveFileName}.uploading");
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

                    var progress = Math.Ceiling(Decimal.Divide(totalRead, file.Stream.Length) * 100);
                    OnUploadProgress((int)progress);
                }
                writeStream.Close();
                if (ct.IsCancellationRequested)
                {
                    if (File.Exists(saveFilePath)) File.Delete(saveFilePath);
                    OnUploadProgress(0);
                    return FileUploadResponse.Fail("Fild upload have been cancelled");
                };
                // remove uploding...
                File.Move(saveFilePath, saveFilePath.Replace(".uploading", ""), true);
                OnUploadProgress(100);

                return FileUploadResponse.Success(saveFileName, file.Stream.Length);

            }
            catch (Exception ex)
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

        public async Task<bool> DeleteProfileImageAsync(string imageName)
        {
            var profileImage = Path.Combine(FileDirectoryProvider.GetProfileImageDirectory(), imageName);
            if (File.Exists(profileImage))
            {
                File.Delete(profileImage);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteProfileImageThumbnailAsync(string imageName)
        {
            var thumbnailImage = Path.Combine(FileDirectoryProvider.GetProfileImageThumbnailsDirectory(), imageName);
            if (File.Exists(thumbnailImage))
            {
                File.Delete(thumbnailImage);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAttachmentsAsync(string[] fileNames)
        {
            foreach(var fileName in fileNames)
            {
                var deleteFile = Path.Combine(FileDirectoryProvider.GetAttachmentDirectory(), fileName);
                if (!File.Exists(deleteFile)) continue;
                try
                {
                    File.Delete(deleteFile);
                }
                catch (Exception)
                {
                    return await Task.FromResult(false);
                }
            }
            
            return await Task.FromResult(true);
        }
    }
}
