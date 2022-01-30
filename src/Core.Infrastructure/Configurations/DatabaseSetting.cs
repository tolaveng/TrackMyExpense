using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Infrastructure.Configurations
{
    // http://www.npgsql.org/doc/connection-string-parameters.html
    public class DatabaseSetting
    {
        [Required]
        public string Host { get; set; }

        [Required]
        public string Port { get; set; }

        [Required]
        public string Database { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set; }

        public string Timeout { get; set; }

        public string CommandTimout { get; set; }

        public string MinPoolSize { get; set; }

        public string MaxPoolSize { get; set; }

        public string MaxAutoPrepare { get; set; }

        public string AutoPepareMinUsage { get; set; }

        public string KeepAlive { get; set; }
    }

    public static class DatabaseConnection
    {
        public static string GetConnectionString(DatabaseSetting setting)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Host={setting.Host};");
            stringBuilder.Append($"Port={setting.Port};");
            stringBuilder.Append($"Database={setting.Database};");
            stringBuilder.Append($"User ID={setting.User};");
            stringBuilder.Append($"Password={setting.Password};");

            if (!string.IsNullOrWhiteSpace(setting.Timeout)) stringBuilder.Append($"Timeout={setting.Timeout};");
            if (!string.IsNullOrWhiteSpace(setting.CommandTimout)) stringBuilder.Append($"Command Timeout={setting.CommandTimout};");
            if (!string.IsNullOrWhiteSpace(setting.MinPoolSize)) stringBuilder.Append($"Minimum Pool Size={setting.MinPoolSize};");
            if (!string.IsNullOrWhiteSpace(setting.MaxPoolSize)) stringBuilder.Append($"Maximum Pool Size={setting.MaxPoolSize};");
            if (!string.IsNullOrWhiteSpace(setting.MaxAutoPrepare)) stringBuilder.Append($"Max Auto Prepare={setting.MaxAutoPrepare};");
            if (!string.IsNullOrWhiteSpace(setting.AutoPepareMinUsage)) stringBuilder.Append($"Auto Prepare Min Usages={setting.AutoPepareMinUsage};");
            if (!string.IsNullOrWhiteSpace(setting.KeepAlive)) stringBuilder.Append($"Keepalive={setting.KeepAlive}");

            //Console.WriteLine("DB connection string: " + stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }
}
