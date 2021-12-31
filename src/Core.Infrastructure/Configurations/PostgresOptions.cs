using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Configurations
{
    // http://www.npgsql.org/doc/connection-string-parameters.html
    public class PostgresOptions
    {
        [Required]
        public string POSTGRES_HOST { get; set; }

        [Required]
        public string POSTGRES_PORT { get; set; }

        [Required]
        public string POSTGRES_DB { get; set; }

        [Required]
        public string POSTGRES_USER { get; set; }

        [Required]
        public string POSTGRES_PASSWORD { get; set; }


        public string POSTGRES_TIMEOUT { get; set; }

        public string POSTGRES_COMMAND_TIMEOUT { get; set; }

        public string POSTGRES_MINIMUM_POOL_SIZE { get; set; }

        public string POSTGRES_MAXIMUM_POOL_SIZE { get; set; }

        public string POSTGRES_MAX_AUTO_PREPARE { get; set; }

        public string POSTGRES_AUTO_PREPARE_MIN_USAGES { get; set; }

        public string POSTGRES_KEEPALIVE { get; set; }
    }
}
