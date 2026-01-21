namespace NTierArchitecture.Entity.Abstractions;

public abstract class AbstractEntity
{
    protected AbstractEntity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? UpdatedUserId { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedUserId { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
