using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Providers.IProviders
{
    public interface IFileDirectoryProvider
    {
        string GetIconDirectory();
        string ResolveIconUrl(IconType iconType, string path);
    }
}
