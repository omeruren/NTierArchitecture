using NTierArchitecture.Entity.Dtos.Abstract;

namespace NTierArchitecture.Entity.Dtos.Category;

public sealed class CategoryResultDto : AbstractEntityDto
{
    public string Name { get; set; } = default!;
}