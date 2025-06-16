namespace KeyValueClient;

public interface IKeyValueStoreClient
{
    Task<string> GetAsync(string key);
    Task<string> SetAsync(string key, string value);
    Task<string> RemoveAsync(string key);
    Task<string> PingAsync();
    Task<IEnumerable<string>> ListAsync();
}
