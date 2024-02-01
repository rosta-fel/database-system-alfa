namespace DatabaseSystemAlfa.Library.Configuration.Structs;

public struct DatabaseConfig
{
    public string Host { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public string Name { get; init; }
    public int Port { get; init; }
}