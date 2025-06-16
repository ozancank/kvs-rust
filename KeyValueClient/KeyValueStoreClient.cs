using System.Net.Sockets;
using System.Text;

namespace KeyValueClient;

public class KeyValueStoreClient(KeyValueStoreEndpoint? keyValueStoreEndpoint) : IKeyValueStoreClient
{
    private readonly KeyValueStoreEndpoint _keyValueStoreEndpoint = keyValueStoreEndpoint ?? KeyValueStoreEndpoint.Default;

    public Task<string> GetAsync(string key) => SendRequestAsync($"GET {key}");

    public Task<string> SetAsync(string key, string value) => SendRequestAsync($"SET {key} {value}");

    public async Task<IEnumerable<string>> ListAsync()
    {
        var message = await SendRequestAsync("LIST");
        var array = message.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);
        return array.Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x));
    }

    public Task<string> PingAsync() => SendRequestAsync("PING");

    public Task<string> RemoveAsync(string key) => SendRequestAsync($"REMOVE {key}");

    private async Task<string> SendRequestAsync(string message)
    {
        using var client = new TcpClient();
        ArgumentNullException.ThrowIfNull(_keyValueStoreEndpoint!.Host, nameof(_keyValueStoreEndpoint));
        await client.ConnectAsync(_keyValueStoreEndpoint.Host, _keyValueStoreEndpoint.Port);
        if (!client.Connected)
        {
            throw new InvalidOperationException($"Could not connect to {_keyValueStoreEndpoint}");
        }

        using var stream = client.GetStream();
        var data = Encoding.UTF8.GetBytes(message + "\n");
        await stream.WriteAsync(data);

        var buffer = new byte[1024];
        var bytesRead = await stream.ReadAsync(buffer);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }
}
