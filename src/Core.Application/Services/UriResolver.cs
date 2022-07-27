using Azure.Storage.Blobs;
using Core.Application.Services.IServices;
using Core.Application.Settings;
using Core.Application.Utils;
using Core.Domain.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UriResolver : IUriResolver
    {
        public readonly AzureStorageSetting AzureSetting;
        private readonly FileUploadSetting FileUploadSetting;

        public UriResolver(IOptions<AzureStorageSetting> azureOptions,
            IOptions<FileUploadSetting> fileUploadOptions)
        {
            AzureSetting = azureOptions.Value;
            FileUploadSetting = fileUploadOptions.Value;
        }

        private string GetUploadDirectoryUrl(string[] subDirectories, string baseUri)
        {
            var url = FileUploadSetting.UploadWebUrl;
            if (!string.IsNullOrEmpty(baseUri))
            {
                url = $"{baseUri.TrimEnd('/')}{url}";
            }

            foreach (var subDirectory in subDirectories)
            {
                if (string.IsNullOrEmpty(subDirectory)) continue;
                url = $"{url}/{subDirectory}";
            }

            return url;
        }

        private string GetAzureBlogUrl(string containerName, string subPath, string fileName, bool useSASToken)
        {
            if (!useSASToken)
            {
                // https://trackingsandbox.blob.core.windows.net/profileimages/68b59f54-fe8c-4a03-b597-c4771560c60b.png
                var url = $"{AzureSetting.DefaultEndpointsProtocol}://{AzureSetting.AccountName}.blob.";
                url += AzureSetting.EndpointSuffix.TrimEnd('/');
                url += $"/{containerName.ToLower()}";
                if (!string.IsNullOrEmpty(subPath)) url += $"/{subPath.ToLower()}";
                url += $"/{fileName}";
                return url;
            }

            var containerClient = new BlobContainerClient(AzureUtil.MakeConnectionString(AzureSetting),
                containerName);
            var blobName = !string.IsNullOrEmpty(subPath) ? $"{subPath}/{fileName}" : fileName;
            var blobClient = containerClient.GetBlobClient(blobName);
            if (blobClient == null) return string.Empty;

            var uri = AzureUtil.GetServiceSasUriForBlob(blobClient);
            if (uri == null) return string.Empty;

            return uri.ToString();
        }

        public string AssetUrl(string fileName, string baseUrl)
        {
            return $"{baseUrl}/assets/{fileName}";
        }

        public string IconUrl(IconType iconType, string path)
        {
            if (string.IsNullOrEmpty(path)) return "";
            
            if (iconType == IconType.Upload)
            {
                if (AzureSetting.IsEnabled)
                {
                    return GetAzureBlogUrl("icons", "", path, false);
                } else
                {
                    return $"{GetUploadDirectoryUrl(new[] { "icons" }, string.Empty)}{path}";
                }
            }
            return path;
        }

        public string AttachmentUrl(string fileName, string subPath, string baseUrl)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            if (AzureSetting.IsEnabled)
            {
                return GetAzureBlogUrl("attachments", subPath, fileName, true);
            }

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{GetUploadDirectoryUrl(new[] { "attachments", subPath }, baseUrl)}{fileName}";
        }

        public string ProfileImageThumbnailUrl(string fileName, string baseUrl)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            if (AzureSetting.IsEnabled)
            {
                return GetAzureBlogUrl("profileimages", "thumbnails", fileName, false);
            }

            fileName = $"{fileName}?v={DateTime.Now.ToFileTime()}";
            return $"{GetUploadDirectoryUrl(new[] { "profileimages", "thumbnails" }, baseUrl)}{fileName}";
        }

        public string ProfileImageUrl(string fileName, string baseUrl)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            if (AzureSetting.IsEnabled)
            {
                return GetAzureBlogUrl("profileimages", "", fileName, false);
            }

            return $"{GetUploadDirectoryUrl(new[] { "profileimages" }, baseUrl)}/{fileName}";
        }

        public string UploadAssetUrl(string fileName, string baseUrl)
        {
            throw new NotImplementedException();
        }
    }
}
