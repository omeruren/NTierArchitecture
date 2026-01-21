namespace NTierArchitecture.Entity.Dtos.Abstract;

public abstract class AbstractEntityDto
{
    public Guid Id { get; set; }

    public Guid CreatedUserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedUserName { get; set; } = default!;

    public Guid? UpdatedUserId { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string UpdatedUserName { get; set; } = default!;

    public bool IsDeleted { get; set; }
    public Guid? DeletedUserId { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedUserName { get; set; } = default!;
}
