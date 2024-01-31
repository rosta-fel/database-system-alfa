using System.Reflection;
using DatabaseSystemAlfa.Library.Configuration.Extensions;

namespace DatabaseSystemAlfa.Library.Configuration.Structs;

public class DatabaseConfig : IConfigurable
{
    public string Host { get; }
    public string User { get; }
    public string Password { get; }
    public string Name { get; }
    public int Port { get; }

    public IEnumerable<PropertyInfo> GetProperties()
    {
        return GetType().GetProperties()
            .Where(pi => !pi.IsMarketWith<Attribute>());
    }
}