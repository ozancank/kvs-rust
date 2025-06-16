using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeyValueClient;

public static class DependencyExtensions
{
    public static IServiceCollection AddKeyValueStoreClient(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("KeyValueStore").Get<KeyValueStoreSection>() ?? new KeyValueStoreSection();

        if (string.IsNullOrWhiteSpace(section.Host))
            section.Host = KeyValueStoreEndpoint.Default.Host;

        if (section.Port <= 5000 || section.Port >= 60000)
            section.Port = KeyValueStoreEndpoint.Default.Port;

        var endpoint = new KeyValueStoreEndpoint(section.Host, section.Port);

        services.AddSingleton(endpoint);
        services.AddSingleton<IKeyValueStoreClient, KeyValueStoreClient>();
        return services;
    }

    public static IServiceCollection AddKeyValueStoreClient(this IServiceCollection services, Action<KeyValueStoreEndpoint>? configureEndpoint)
    {
        var endpoint = KeyValueStoreEndpoint.Default;
        configureEndpoint?.Invoke(endpoint);

        services.AddSingleton(endpoint);
        services.AddSingleton<IKeyValueStoreClient, KeyValueStoreClient>();
        return services;
    }
}