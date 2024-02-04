using DatabaseSystemAlfa.Libraries.Configuration.Exceptions;
using DatabaseSystemAlfa.Libraries.Configuration.Settings;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa;

public class AppSettings
{
    public DbConnectionSettings DbConnectionSettings { get; set; }

    public AppSettings()
    {
        DbConnectionSettings = new DbConnectionSettings
        {
            Server = "127.0.0.1",
            Uid = "root",
            Password = "root",
            Database = "database_system_alfa_db",
            Port = 3306
        };
    }

    public AppSettings(IConfiguration conf)
    {
        IConfigurationSection dbConfSection = conf.GetSection(nameof(DbConnectionSettings));

        if (!dbConfSection.GetChildren().Any())
            throw new InvalidConfigException("Invalid or missing section in the configuration file: ", nameof(DbConnectionSettings));

        DbConnectionSettings = dbConfSection.Get<DbConnectionSettings>();
    }

    public string GetConnectionString() => DbConnectionSettings.ToString();
}