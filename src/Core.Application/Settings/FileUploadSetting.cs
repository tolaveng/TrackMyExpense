using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Settings
{
    public class FileUploadSetting
    {
        public double MaxFileSizeKb { get; set; } = 3072;
        public string TempDir { get; set; } = string.Empty;
        public string UploadDir { get; set; } = string.Empty;
        public string UploadWebUrl { get; set; } = string.Empty;
    }
}
