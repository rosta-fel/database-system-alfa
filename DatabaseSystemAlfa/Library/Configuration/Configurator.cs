using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa.Library.Configuration;

public static class Configurator
{
    public const string FileName = "app-settings.json";
    
    public static IEnumerable<Type> GetTypesImplementingInterface<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(T).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });
    }
    
    public static IConfiguration Build()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, FileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file '{FileName}' does not exist at the specified path: {currentDirectory}");

        return new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile(FileName, true, false)
            .Build();
    }
}