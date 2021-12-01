using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database
{
    public interface IArchivable
    {
        bool Archived { get; set; }
        DateTimeOffset? ArchivedAt { get; set; }
    }
}
