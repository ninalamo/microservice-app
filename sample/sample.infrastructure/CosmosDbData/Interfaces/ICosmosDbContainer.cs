namespace sample.infrastructure.CosmosDbData.Interfaces;


public interface ICosmosDbContainer
{
    Container _container { get; }
}