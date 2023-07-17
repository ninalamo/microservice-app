namespace sample.infrastructure.CosmosDbData.Interfaces;

public interface ICosmosDbContainerFactory
{
    ICosmosDbContainer GetContainer(string? containerName);

    Task EnsureDbSetupAsync();
}