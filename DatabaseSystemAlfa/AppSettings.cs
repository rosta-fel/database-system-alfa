using DatabaseSystemAlfa.Libraries.Configuration.Exceptions;
using DatabaseSystemAlfa.Libraries.Configuration.Settings;
using Microsoft.Extensions.Configuration;

namespace DatabaseSystemAlfa;

/// <summary>
///     Represents application settings including database connection settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    ///     Gets or sets the database connection settings.
    /// </summary>
    public DbConnectionSettings DbConnectionSettings { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AppSettings" /> class with default database connection settings.
    /// </summary>
    public AppSettings()
    {
        DbConnectionSettings = new DbConnectionSettings
        {
            Server = "127.0.0.1",
            Uid = "root",
            Password = "root",
            Database = "database_system_alfa_db",
            Port = 3306
        };
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AppSettings" /> class using the provided configuration.
    /// </summary>
    /// <param name="conf">The <see cref="IConfiguration" /> containing application settings.</param>
    /// <exception cref="InvalidConfigException">
    ///     Thrown when the configuration section for database settings is missing or
    ///     invalid.
    /// </exception>
    public AppSettings(IConfiguration conf)
    {
        var dbConfSection = conf.GetSection(nameof(DbConnectionSettings));

        if (!dbConfSection.GetChildren().Any())
            throw new InvalidConfigException("Invalid or missing section in the configuration file: ",
                nameof(DbConnectionSettings));

        DbConnectionSettings = dbConfSection.Get<DbConnectionSettings>();
    }

    /// <summary>
    ///     Gets the connection string based on the configured database connection settings.
    /// </summary>
    /// <returns>The connection string for the database.</returns>
    public string GetConnectionString()
    {
        return DbConnectionSettings.ToString();
    }
}