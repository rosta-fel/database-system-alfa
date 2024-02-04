using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.Services;

/// <summary>
///     Represents a singleton class for managing a MySQL database connection.
/// </summary>
public sealed class DatabaseSingleton
{
    private static readonly Lazy<DatabaseSingleton> LazyInstance = new(() => new DatabaseSingleton());

    /// <summary>
    ///     Gets the singleton instance of the <see cref="DatabaseSingleton" /> class.
    /// </summary>
    public static DatabaseSingleton Instance => LazyInstance.Value;

    /// <summary>
    ///     Gets or sets the MySQL database connection.
    /// </summary>
    public MySqlConnection? Connection { get; private set; }

    /// <summary>
    ///     Private constructor to ensure a single instance of the <see cref="DatabaseSingleton" /> class.
    /// </summary>
    private DatabaseSingleton()
    {
    }

    /// <summary>
    ///     Initializes the database connection with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string for the MySQL database.</param>
    public void Initialize(string connectionString)
    {
        Connection = new MySqlConnection(connectionString);
    }

    /// <summary>
    ///     Closes the database connection if it is open.
    /// </summary>
    public void CloseConnection()
    {
        try
        {
            if (Connection is { State: ConnectionState.Open }) Connection.Close();
        }
        finally
        {
            Connection = null;
        }
    }

    /// <summary>
    ///     Checks if the database connection is open.
    /// </summary>
    /// <returns><c>true</c> if the connection is open; otherwise, <c>false</c>.</returns>
    public bool ConnectionIsOpen()
    {
        return Connection is { State: ConnectionState.Open };
    }
}