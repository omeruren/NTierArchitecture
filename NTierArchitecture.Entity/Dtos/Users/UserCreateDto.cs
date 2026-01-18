namespace NTierArchitecture.Entity.Dtos.Users;

public sealed record UserCreateDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password);
