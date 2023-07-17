using Microsoft.Extensions.Configuration;
using sample.infrastructure.AppSettings;

namespace sample.infrastructure.CosmosDbData.Constants;

public class ConfigConstants : IConfigConstants
{
    public IConfiguration Configuration { get; }
    private readonly CosmosDbSettings cosmosDbConfig;

    public ConfigConstants(IConfiguration configuration, CosmosDbSettings cosmosDbConfig)
    {
        this.Configuration = configuration;
        this.cosmosDbConfig = cosmosDbConfig;
        var temp = this.Configuration
            .GetSection("ConnectionStrings:CosmosDbSettings");
            //.GetValue<CosmosDbSettings>();
    }

    public string AUDIT_CONTAINER => this.cosmosDbConfig?.Containers[0]?.Name;

    public string USER_CONTAINER => this.cosmosDbConfig?.Containers[1]?.Name;
}