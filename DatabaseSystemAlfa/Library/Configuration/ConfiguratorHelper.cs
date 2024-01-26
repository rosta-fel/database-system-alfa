using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Library.Configuration;

public static class ConfiguratorHelper
{
    public static AppSettings LoadAppSettings(string configFileName)
    {
        IConfigurationRoot configurationRoot = Configurator.Build(configFileName);
        return new AppSettings(configurationRoot);
    }
}