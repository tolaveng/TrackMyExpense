using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database
{
    public class AppDbContextConfigurator : IAppDbContextConfigurator
    {
        private readonly PostgresOptions _postgresOptions;
        public AppDbContextConfigurator(IOptions<PostgresOptions> postgresOptions)
        {
            _postgresOptions = postgresOptions.Value;
        }
        public DbContextOptionsBuilder Configure(DbContextOptionsBuilder optionsBuilder)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Host={_postgresOptions.POSTGRES_HOST};");
            stringBuilder.Append($"Port={_postgresOptions.POSTGRES_PORT};");
            stringBuilder.Append($"Database={_postgresOptions.POSTGRES_DB};");
            stringBuilder.Append($"User ID={_postgresOptions.POSTGRES_USER};");
            stringBuilder.Append($"Password={_postgresOptions.POSTGRES_PASSWORD};");

            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_TIMEOUT)) stringBuilder.Append($"Timeout={_postgresOptions.POSTGRES_TIMEOUT};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_COMMAND_TIMEOUT)) stringBuilder.Append($"Command Timeout={_postgresOptions.POSTGRES_COMMAND_TIMEOUT};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_MINIMUM_POOL_SIZE)) stringBuilder.Append($"Minimum Pool Size={_postgresOptions.POSTGRES_MINIMUM_POOL_SIZE};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_MAXIMUM_POOL_SIZE)) stringBuilder.Append($"Maximum Pool Size={_postgresOptions.POSTGRES_MAXIMUM_POOL_SIZE};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_MAX_AUTO_PREPARE)) stringBuilder.Append($"Max Auto Prepare={_postgresOptions.POSTGRES_MAX_AUTO_PREPARE};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_AUTO_PREPARE_MIN_USAGES)) stringBuilder.Append($"Auto Prepare Min Usages={_postgresOptions.POSTGRES_AUTO_PREPARE_MIN_USAGES};");
            if (!string.IsNullOrWhiteSpace(_postgresOptions.POSTGRES_KEEPALIVE)) stringBuilder.Append($"Keepalive={_postgresOptions.POSTGRES_KEEPALIVE}");

            Console.WriteLine("DB connection string: " + stringBuilder.ToString());
            return optionsBuilder.UseNpgsql(stringBuilder.ToString());
        }
    }
}
