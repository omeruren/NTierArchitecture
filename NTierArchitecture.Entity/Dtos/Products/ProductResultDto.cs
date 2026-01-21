using NTierArchitecture.Entity.Dtos.Abstract;

namespace NTierArchitecture.Entity.Dtos.Products;

public sealed class ProductResultDto : AbstractEntityDto
{
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}
