using System.Reflection;
using System.Text;

namespace DatabaseSystemAlfa.Libraries.Configuration.Settings;

/// <summary>
/// Represents database connection settings with properties for server, database, user ID, password, and port.
/// </summary>
public readonly struct DbConnectionSettings
{
    /// <summary>
    /// Gets the server name or address for the database connection.
    /// </summary>
    public string Server { get; init; }

    /// <summary>
    /// Gets the name of the database for the connection.
    /// </summary>
    public string Database { get; init; }

    /// <summary>
    /// Gets the user ID for authenticating the database connection.
    /// </summary>
    public string Uid { get; init; }

    /// <summary>
    /// Gets the password for authenticating the database connection.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Gets the port number for the database connection.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbConnectionSettings"/> struct with specified connection details.
    /// </summary>
    /// <param name="server">The server name or address.</param>
    /// <param name="database">The name of the database.</param>
    /// <param name="uid">The user ID for authentication.</param>
    /// <param name="password">The password for authentication.</param>
    /// <param name="port">The port number for the database connection.</param>
    public DbConnectionSettings(string server, string database, string uid, string password, int port)
    {
        Server = server;
        Database = database;
        Uid = uid;
        Password = password;
        Port = port;
    }

    /// <summary>
    /// Generates a connection string representation of the <see cref="DbConnectionSettings"/>.
    /// </summary>
    /// <returns>A connection string representation.</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        PropertyInfo[] properties = GetType().GetProperties();
        foreach (var property in properties)
            builder.Append($"{property.Name}={property.GetValue(this)};");

        return builder.ToString();
    }
}