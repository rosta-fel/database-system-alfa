using DatabaseSystemAlfa.Configuration.Exceptions;
using DatabaseSystemAlfa.Configuration.Structs;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Configuration;

public sealed class AppSettings
{
    public DatabaseConfig DbConfig { get; set; }

    public AppSettings(IConfiguration configuration)
    {
        IConfigurationSection databaseConfigSection = configuration.GetSection(nameof(DatabaseConfig));

        if (!databaseConfigSection.GetChildren().Any())
            throw new InvalidConfigException("Invalid or missing section in the configuration file: {0}", nameof(DatabaseConfig));
        
        DbConfig = databaseConfigSection.Get<DatabaseConfig>();
    }
}