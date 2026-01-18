using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Business.Abstractions;
using NTierArchitecture.Entity.Dtos.Pagination;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Users;

public sealed class UserService(UserManager<User> _userManager)
{
    public async Task<Result<string>> CreateAsync(UserCreateDto request, CancellationToken token)
    {
        bool isEmailExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, token);

        if (isEmailExists)
            throw new ArgumentException("This email is already taken by someone else.");

        User user = User.Create(request.FirstName, request.LastName, request.UserName, request.Email);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new ArgumentException(result.Errors.Select(e => e.Description).First());

        return "User created successfully.";
    }

    public async Task<Result<User>> GetAsync(Guid id, CancellationToken token)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString()) ?? throw new ArgumentException("User not found.");

        return user;
    }

    public async Task<Result<PaginationResponseDto<User>>> GetAllAsync(PaginationRequestDto request, CancellationToken token)
    {
        var result = await _userManager.Users.
            Where(u => u.FullName.Contains(request.Search) || u.Email!.Contains(request.Search))
            .OrderBy(u => u.FullName)
            .Pagination(request, token);
        return result;
    }

    public async Task<Result<string>> UpdateAsync(UserUpdateDto request, CancellationToken token)
    {
        User? user = await _userManager.FindByIdAsync(request.Id.ToString()) ?? throw new ArgumentException("User not found.");

        user.Update(request.FirstName, request.LastName, request.UserName, request.Email);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new ArgumentException(result.Errors.Select(e => e.Description).First());
        return "User Updated successfully.";
    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()) ?? throw new ArgumentException("User not found.");

        if (!user.IsDeleted)
        {
            user.Delete();
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new ArgumentException(result.Errors.Select(e => e.Description).First());
        }
        else
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new ArgumentException(result.Errors.Select(e => e.Description).First());
        }
        return "User Deleted successfully.";
    }
}
