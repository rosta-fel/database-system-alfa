using System.Reflection;
using System.Text;

namespace DatabaseSystemAlfa.Libraries.Configuration.Settings;

public readonly struct DbConnectionSettings(string server, string database, string uid, string password, int port)
{
    public string Server { get; init; } = server;
    public string Database { get; init; } = database;
    public string Uid { get; init; } = uid;
    public string Password { get; init; } = password;
    public int Port { get; init; } = port;


    public override string ToString()
    {
        StringBuilder builder = new();

        PropertyInfo[] properties = GetType().GetProperties();
        foreach (var property in properties)
            builder.Append($"{property.Name}={property.GetValue(this)};");

        return builder.ToString();
    }
}