using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Application.Common.Files;
using Core.Application.Extensions;
using Core.Application.Services.IServices;
using Core.Application.Settings;
using Core.Application.Utils;
using Microsoft.Extensions.Options;

namespace Core.Application.Services
{
    public class AzureFileUploadService : IFileUploadService
    {
        public string[] AllowExtensions { get; set; } = { ".png", ".jpg", ".jpeg" };
        public long MaxFileSize { get; set; } = 100 * 1024; // 100 K
        public AzureStorageSetting setting { get; }
 
        public event EventHandler<int> UploadProgress;

        protected virtual void OnUploadProgress(int progress)
        {
            UploadProgress?.Invoke(this, progress);
        }

        public AzureFileUploadService(IOptions<AzureStorageSetting> options)
        {
            setting = options.Value;
        }

        private string GetConnectionString()
        {
            return AzureUtil.MakeConnectionString(setting);
        }

        public async Task Initialize()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(GetConnectionString());
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(setting.AttachmentContainerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        public Task<bool> DeleteAttachmentsAsync(Guid userId, string[] fileNames)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteIconFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProfileImageAsync(string imageName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProfileImageThumbnailAsync(string imageName)
        {
            throw new NotImplementedException();
        }

        public async Task<FileUploadResponse> SaveAttachmentAsync(Guid userId, FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var containerClient = new BlobContainerClient(GetConnectionString(), setting.AttachmentContainerName);
            try
            {
                await containerClient.CreateIfNotExistsAsync();
            }
            catch (Exception)
            {
                return FileUploadResponse.Fail("Something went wrong. Cannot access to Azure cloud.");
            }

            var result = await UploadFileAsync(containerClient, file, userId.ToString(), saveFileName, ct);

            return result;
        }

        public async Task<FileUploadResponse> SaveIconFileAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var containerClient = new BlobContainerClient(GetConnectionString(), "icons");
            try
            {
                await containerClient.CreateIfNotExistsAsync();
            }
            catch (Exception)
            {
                return FileUploadResponse.Fail("Something went wrong. Cannot access to Azure cloud.");
            }

            var result = await UploadFileAsync(containerClient, file, "", saveFileName, ct);
            return result;
        }

        public async Task<FileUploadResponse> SaveProfileImageAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var containerClient = new BlobContainerClient(GetConnectionString(), setting.ProfileImageContainerName);
            try
            {
                await containerClient.CreateIfNotExistsAsync();
            }catch (Exception)
            {
                return FileUploadResponse.Fail("Something went wrong. Cannot access to Azure cloud.");
            }

            var result = await UploadFileAsync(containerClient, file, "", saveFileName, ct);
            return result;
        }

        public async Task<FileUploadResponse> SaveProfileImageThumbnailAsync(FileUploadRequest file, string saveFileName, CancellationToken ct)
        {
            var containerClient = new BlobContainerClient(GetConnectionString(), setting.ProfileImageContainerName);
            try
            {
                await containerClient.CreateIfNotExistsAsync();
            }
            catch (Exception)
            {
                return FileUploadResponse.Fail("Something went wrong. Cannot access to Azure cloud.");
            }

            return await UploadFileAsync(containerClient, file, "thumbnails", saveFileName, ct);
        }

        #region Cloud
        /// <summary>
        /// Upload file to Azure storage
        /// </summary>
        /// <param name="file">File request stream</param>
        /// <param name="saveDirectoryPath">Represent as a path '/' </param>
        /// <param name="saveFileName">File name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>File Upload Response</returns>
        public async Task<FileUploadResponse> UploadFileAsync(BlobContainerClient containerClient,
            FileUploadRequest file,
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

            var saveFilePath = string.IsNullOrEmpty(saveDirectoryPath)
                ? saveFileName
                : Path.Combine(saveDirectoryPath, saveFileName);
            var contentType = file.ContentType ?? saveFileName.GetContentType();
            var blobClient = containerClient.GetBlobClient(saveFilePath);
            if (blobClient == null)
            {
                return FileUploadResponse.Fail("Somthing went wrong, cannot access to the cloud.");
            }

            try
            {
                await blobClient.DeleteIfExistsAsync();

                var response = await blobClient.UploadAsync(file.Stream, new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = contentType },
                    TransferOptions = new StorageTransferOptions
                    {
                        InitialTransferSize = 1024 * 1024,
                        MaximumConcurrency = 8
                    },
                    ProgressHandler = new Progress<long>((progress) =>
                    {
                        var progressing = Math.Ceiling(Decimal.Divide(progress, file.Stream.Length) * 100);
                        OnUploadProgress((int)progressing);
                    })
                });

                
                if (ct.IsCancellationRequested)
                {
                    await blobClient.DeleteIfExistsAsync();
                    OnUploadProgress(0);
                    return FileUploadResponse.Fail("Fild upload have been cancelled");
                };

                OnUploadProgress(100);

                return FileUploadResponse.Success(saveFileName, file.Stream.Length);

            }
            catch (Exception ex)
            {
                OnUploadProgress(0);
                await blobClient.DeleteIfExistsAsync();
                return FileUploadResponse.Fail(ex.Message);
            }
        }


        
        #endregion;
    }
}
