using Microsoft.AspNetCore.Identity;

namespace NTierArchitecture.Entity.Models;

public sealed class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName { get; private set; } = default!;

    public static User Create(string firstName, string lastName, string userName, string fullName, string email)
    {
        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            FullName = firstName + " " + lastName,
            UserName = userName,
            Email = email
        };
    }
    public void Update(string firstName, string lastName, string userName, string fullName, string email)
    {

        FirstName = firstName;
        LastName = lastName;
        FullName = firstName + " " + lastName;
        UserName = userName;
        Email = email;
    }

}
