namespace NTierArchitecture.Entity.Dtos.Products;

public record ProductCreateDto(string Name, decimal UnitPrice, Guid CategoryId);
