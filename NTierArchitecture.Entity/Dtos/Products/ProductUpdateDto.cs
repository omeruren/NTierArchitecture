namespace NTierArchitecture.Entity.Dtos.Products;

public record ProductUpdateDto(Guid Id, string Name, decimal UnitPrice, Guid CategoryId);
