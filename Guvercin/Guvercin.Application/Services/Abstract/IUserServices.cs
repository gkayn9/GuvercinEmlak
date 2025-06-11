using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Dtos.UserDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IUserServices
{
    Task<ResponseDto<object>> Register(RegisterDto registerDto);
    Task<ResponseDto<object>> CreateRole(string roleName);
    Task<ResponseDto<object>> AddToRole(string email, string roleName);
}