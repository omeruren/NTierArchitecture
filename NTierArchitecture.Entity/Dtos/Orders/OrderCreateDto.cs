namespace NTierArchitecture.Entity.Dtos.Orders;

public sealed record OrderCreateDto(Guid ProductId, int Quantity);