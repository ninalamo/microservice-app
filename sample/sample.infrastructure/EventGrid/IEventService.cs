using Azure;
using Azure.Messaging.EventGrid;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;

namespace sample.infrastructure.EventGrid;

public interface IEventService
{
    Task SendAsync(List<EventGridEvent> events);
}

public class EventService : IEventService
{
    private readonly EventGridPublisherClient _client;

    public EventService(string topicEndpoint, string topicAccessKey)
    {
        _client = new EventGridPublisherClient(
            new Uri(topicEndpoint),
            new AzureKeyCredential(topicAccessKey));
    }
    public async Task SendAsync(List<EventGridEvent> events)
    {


        await _client.SendEventsAsync(events);
    }
}