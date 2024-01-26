using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Library.Configuration;

public static class Configurator
{
    public static IConfigurationRoot Build(string configurationFileName)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, configurationFileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file '{configurationFileName}' does not exist at the specified path: {currentDirectory}");

        return new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile(configurationFileName, true, false)
            .Build();
    }
}