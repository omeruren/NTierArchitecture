namespace NTierArchitecture.Entity.Dtos.Users;

public sealed record UserUpdateDto(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password);
