using System.Reflection;
using System.Text;

namespace DatabaseSystemAlfa.Libraries.Configuration.Settings;

public readonly struct DbConnectionSettings
{
    public string Server { get; init; }
    public string Database { get; init; }
    public string Uid { get; init; }
    public string Password { get; init; }
    public int Port { get; init; }


    public override string ToString()
    {
        StringBuilder builder = new();

        PropertyInfo[] properties = GetType().GetProperties();
        foreach (var property in properties)
            builder.Append($"{property.Name}={property.GetValue(this)};");

        return builder.ToString();
    }
}