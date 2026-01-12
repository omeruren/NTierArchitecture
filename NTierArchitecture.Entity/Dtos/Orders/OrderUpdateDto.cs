namespace NTierArchitecture.Entity.Dtos.Orders;

public record OrderUpdateDto(Guid Id, Guid ProductId, int Quantity);
