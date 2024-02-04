using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Libraries.Configuration;

/// <summary>
///     A static class responsible for configuring and managing application settings through a JSON configuration file.
/// </summary>
public static class Configurator
{
    /// <summary>
    ///     Gets the current working directory of the application.
    /// </summary>
    private static string WorkingDirectory { get; } = Directory.GetCurrentDirectory();

    /// <summary>
    ///     Gets or sets the name of the configuration file (default is "config.json").
    /// </summary>
    public static string ConfigFileName { get; set; } = "config.json";

    /// <summary>
    ///     Retrieves the validated full path of the configuration file.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the configuration file name is invalid or not declared.</exception>
    /// <returns>The full path of the configuration file.</returns>
    private static string GetValidatedConfigFilePath()
    {
        if (string.IsNullOrWhiteSpace(ConfigFileName))
            throw new ArgumentException("Configuration file name is invalid or not declared", nameof(ConfigFileName));

        return Path.Combine(WorkingDirectory, ConfigFileName);
    }

    /// <summary>
    ///     Initializes a new <see cref="FileNotFoundException" /> with the JSON configuration file.
    /// </summary>
    /// <exception cref="IConfigurationBuilder">Thrown when the specified configuration file is not found.</exception>
    /// <returns>An <see cref="IConfigurationBuilder" /> instance configured with the JSON file.</returns>
    public static IConfigurationBuilder InitBuilder()
    {
        var filePath = GetValidatedConfigFilePath();

        if (!File.Exists(filePath))
            throw new FileNotFoundException(
                $"The config file '{ConfigFileName}' does not exist at the specified path: {WorkingDirectory}");

        return new ConfigurationBuilder().AddJsonFile(ConfigFileName, true, false);
    }

    /// <summary>
    ///     Serializes an object to JSON and writes it to the configuration file.
    /// </summary>
    /// <param name="instance">The object to be serialized to JSON.</param>
    /// <param name="prettyPrint">Indicates whether to format the JSON with indentation.</param>
    public static void SerializeToJson(object instance, bool prettyPrint)
    {
        var filePath = GetValidatedConfigFilePath();

        var json = JsonSerializer.Serialize(instance, new JsonSerializerOptions
        {
            WriteIndented = prettyPrint
        });

        File.WriteAllText(filePath, json);
    }
}