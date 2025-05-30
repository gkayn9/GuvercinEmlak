using Guvercin.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace Guvercin.Application.Interfaces;

public interface IUserRepository
{
    Task<SignInResult> LoginAsync(LoginDto dto);
    Task LogoutAsync();
    Task<IdentityResult> RegisterAsync(RegisterDto dto);
    Task<UserDto>CheckUser (string email);
    Task<SignInResult> CheckUserWithPassword(LoginDto dto);
}