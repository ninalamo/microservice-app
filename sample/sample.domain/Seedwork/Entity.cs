using Newtonsoft.Json;

namespace sample.domain.Seedwork;

public abstract class Entity : IAuditable
{
    [JsonProperty(PropertyName= "id")]
    public virtual string? Id { get; set; }
    
    [JsonProperty(PropertyName = "partitionKey")]
    public string? PartitionKey { get; set; }


    public bool IsTransient() => Id == default;


    //implementation of IAuditable
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTimeOffset CreatedOn { get; private set; } = DateTimeOffset.Now;
    public string ModifiedBy { get; private set; } = string.Empty;
    public DateTimeOffset ModifiedOn { get; private set; } = DateTimeOffset.Now;
    public bool IsActive { get; set; } = true;

  
}