using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.Services
{
    public sealed class DatabaseSingleton
    {
        private static readonly Lazy<DatabaseSingleton> LazyInstance = new(() => new DatabaseSingleton());
        public static DatabaseSingleton Instance => LazyInstance.Value;
        public MySqlConnection? Connection { get; private set; }

        private DatabaseSingleton() { }
        
        public void Initialize(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

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
    }
}