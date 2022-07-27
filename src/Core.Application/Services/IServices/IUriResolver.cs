using Core.Domain.Enums;

namespace Core.Application.Services.IServices
{
    public interface IUriResolver
    {
        public string AssetUrl(string fileName, string baseUrl);
        public string IconUrl(IconType iconType, string path);
        public string ProfileImageUrl(string fileName, string baseUrl);
        public string ProfileImageThumbnailUrl(string fileName, string baseUrl);
        public string AttachmentUrl(string fileName, string subPath, string baseUrl);
    }
}
