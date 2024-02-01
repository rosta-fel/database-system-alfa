using DatabaseSystemAlfa.Library.Configuration.Exceptions;
using DatabaseSystemAlfa.Library.Configuration.Structs;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa;

public class AppSettings
{
    public DatabaseConfig DatabaseConfig { get; }

    public AppSettings()
    {
        DatabaseConfig = new DatabaseConfig
        {
            Host = "localhost",
            User = "root",
            Port = 3306
        };
    }

    public AppSettings(IConfiguration conf)
    {
        IConfigurationSection dbConfSection = conf.GetSection(nameof(DatabaseConfig));

        if (!dbConfSection.GetChildren().Any())
            throw new InvalidConfigException("Invalid or missing section in the configuration file: ", nameof(DatabaseConfig));

        DatabaseConfig = dbConfSection.Get<DatabaseConfig>();
    }
}