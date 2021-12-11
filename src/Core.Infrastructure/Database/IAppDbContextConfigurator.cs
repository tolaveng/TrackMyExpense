using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database
{
    public interface IAppDbContextConfigurator
    {
        DbContextOptionsBuilder Configure(DbContextOptionsBuilder optionsBuilder);
    }
}
