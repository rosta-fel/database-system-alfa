namespace DatabaseSystemAlfa.Library.Configuration.Structs;

public record struct DatabaseConfig
{
    private readonly string _host;
    private readonly string _user;
    private readonly string _name;
    private readonly int _port;

    public DatabaseConfig(string host, string user, string password, string name, int port)
    {
        Host = host;
        User = user;
        Password = password;
        Name = name;
        Port = port;
    }
    
    public string Host
    {
        get => _host;
        [System.Diagnostics.CodeAnalysis.MemberNotNull("_host")]
        init
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Server database host cannot be null or empty.", nameof(Host));

            _host = value;
        }
    }
    
    public string User
    {
        get => _user;
        [System.Diagnostics.CodeAnalysis.MemberNotNull("_user")]
        init
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Database user cannot be null or empty.", nameof(User));

            _user = value;
        }
    }
    
    public string Password { get; init; }

    public string Name
    {
        get => _name;
        [System.Diagnostics.CodeAnalysis.MemberNotNull("_name")]
        init
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Database name cannot be null or empty.", nameof(Name));

            _name = value;
        }
    }
    
    public int Port
    {
        get => _port;
        init
        {
            if (value is <= 0 or > 65535)
                throw new ArgumentOutOfRangeException(nameof(Port), "Database port must be in the range 1 to 65535.");

            _port = value;
        }
    }
}