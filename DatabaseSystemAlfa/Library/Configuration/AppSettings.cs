using System.Text.Json;
using DatabaseSystemAlfa.Library.Configuration.Exceptions;
using DatabaseSystemAlfa.Library.Configuration.Extensions;
using DatabaseSystemAlfa.Library.Tools.Json;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Library.Configuration;

public sealed class AppSettings
{
    public Dictionary<string, IConfigurable> Config { get; }

    public AppSettings()
    {
        Config = new Dictionary<string, IConfigurable>();
        DefaultInitialization();
    }

    public AppSettings(IConfiguration configuration) : this()
    {
        TryLoadConfiguration(configuration, out List<string> missingSections);
        
        if (missingSections.Count != 0)
            throw new InvalidConfigException("Invalid or missing sections in the configuration file: {0}",
                string.Join(", ", missingSections));
    }

    private void TryLoadConfiguration(IConfiguration configuration, out List<string> missingSections)
    {
        missingSections = [];

        foreach (var (key, value) in Config)
        {
            var configSection = configuration.GetSection(key);

            if (!configSection.GetChildren().Any())
            {
                missingSections.Add(key);
            }
            else
            {
                var valueObject = configSection.Get(value.GetType());

                if (valueObject != null)
                    Config[key] = (IConfigurable)valueObject;
                else
                    throw new InvalidConfigException("Failed to convert configuration section for: {0}", key);
            }
        }
    }
    
    private void DefaultInitialization()
    {
        IEnumerable<Type> configurableTypes = Configurator.GetTypesImplementingInterface<IConfigurable>();

        foreach (var configurableType in configurableTypes)
        {
            var configurableInstance = Activator.CreateInstance(configurableType)!;
            Config.Add(configurableType.Name, (IConfigurable)configurableInstance);
        }
    }

    public static void SaveTo(string configFileName, AppSettings instance)
    {
        ArgumentNullException.ThrowIfNull(instance);

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), configFileName);

        var json = JsonSerializer.Serialize(instance, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new AppSettingsConverter() }
        });

        File.WriteAllText(fullPath, json);
    }
}