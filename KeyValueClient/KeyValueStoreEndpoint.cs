namespace KeyValueClient;

public class KeyValueStoreEndpoint
{
    public string? Host { get; set; }
    public int Port { get; set; }

    public KeyValueStoreEndpoint(string? host, int port)
    {
        Host = host ?? throw new ArgumentNullException(nameof(host));
        if (port <= 5000 || port >= 60000)
            throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 5000 and 60000.");
        Port = port;
    }

    public override string ToString()
    {
        return $"{Host}:{Port}";
    }

    public static KeyValueStoreEndpoint Default => new("127.0.0.1", 5544);
}
