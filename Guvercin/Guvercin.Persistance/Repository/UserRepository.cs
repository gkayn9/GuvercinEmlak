using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Domain.Entities;
using Guvercin.Persistance.Context.Identity;
using Microsoft.AspNetCore.Identity;

namespace Guvercin.Persistance.Repository;

public class UserRepository: IUserRepository
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly SignInManager<AppIdentityUser> _signInManager;
    
    public UserRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        return result;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        var user = new AppIdentityUser
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            UserName = dto.Email
        };
        var result = _userManager.CreateAsync(user, dto.Password);
        return result;
    }

    public async Task<UserDto> CheckUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return new UserDto { Id = user.Id, Email = user.Email };
        }
        return new UserDto();
    }

    public async Task<SignInResult> CheckUserWithPassword(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        var result =await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
        return result;
    }
}