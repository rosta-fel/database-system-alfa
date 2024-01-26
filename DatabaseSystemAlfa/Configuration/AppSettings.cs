using System.Text.Json;
using DatabaseSystemAlfa.Configuration.Exceptions;
using DatabaseSystemAlfa.Configuration.Structs;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Configuration;

public sealed class AppSettings
{
    public DatabaseConfig DatabaseConfig { get; set; }
    
    public AppSettings(DatabaseConfig databaseConfig)
    {
        DatabaseConfig = databaseConfig;
    }

    public AppSettings(IConfiguration configuration)
    {
        IConfigurationSection databaseConfigSection = configuration.GetSection(nameof(Structs.DatabaseConfig));

        if (!databaseConfigSection.GetChildren().Any())
            throw new InvalidConfigException("Invalid or missing section in the configuration file: {0}", nameof(Structs.DatabaseConfig));
        
        DatabaseConfig = databaseConfigSection.Get<DatabaseConfig>();
    }

    public static void SaveTo(string configFileName, AppSettings instance)
    {
        ArgumentNullException.ThrowIfNull(instance);
        
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), configFileName);

        string json = JsonSerializer.Serialize(instance, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        File.WriteAllText(fullPath, json);
    }
}