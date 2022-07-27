using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Settings
{
    public class AzureStorageSetting
    {
        public bool IsEnabled { get; set; }
        public string DefaultEndpointsProtocol { get; set; } = string.Empty;
        public string EndpointSuffix { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountKey { get; set; } = string.Empty;

        public string AttachmentContainerName { get; set; } = string.Empty;
        public string ProfileImageContainerName { get; set; } = string.Empty;
    }
}
