using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Libraries.Configuration;

public static class Configurator
{
    private static string WorkingDirectory { get; } = Directory.GetCurrentDirectory();
    public static string ConfigFileName { get; set; } = "config.json";
    
    public static IConfigurationBuilder InitBuilder()
    {
        if (string.IsNullOrWhiteSpace(ConfigFileName))
            throw new ArgumentException("Configuration file name is invalid or not declared", nameof(ConfigFileName));
        
        string filePath = Path.Combine(WorkingDirectory, ConfigFileName);
        
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The config file '{ConfigFileName}' does not exist at the specified path: {WorkingDirectory}");

        return new ConfigurationBuilder().AddJsonFile(ConfigFileName, true, false);
    }

    public static void SerializeToJson(object instance, bool prettyPrint)
    {
        if (string.IsNullOrWhiteSpace(ConfigFileName))
            throw new ArgumentException("Configuration file name is invalid or not declared", nameof(ConfigFileName));
        
        string filePath = Path.Combine(WorkingDirectory, ConfigFileName);

        string json = JsonSerializer.Serialize(instance, new JsonSerializerOptions
        {
            WriteIndented = prettyPrint
        });
        
        File.WriteAllText(filePath, json);
    }
}