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
    private readonly RoleManager<AppIdentityRole> _roleManager;
    
    public UserRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager, RoleManager<AppIdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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

    public async Task<bool> CreateRoleAsync(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            return false;
        }
       var roleExist=await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
        {
            return false;
        }
        var result =await _roleManager.CreateAsync(new AppIdentityRole{ Name=roleName});
        if (result.Succeeded)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> AddRoleToUserAsync(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false;
        }
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
        {
            var result = await _userManager.AddToRoleAsync( user, roleName);
            if (result.Succeeded)
            {return true;}
        }
        return false;
    }
}