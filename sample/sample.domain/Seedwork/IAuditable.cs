public interface IAuditable
{
    public string CreatedBy { get; }
    public DateTimeOffset CreatedOn { get; }
    public string ModifiedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public bool IsActive { get; }

}

