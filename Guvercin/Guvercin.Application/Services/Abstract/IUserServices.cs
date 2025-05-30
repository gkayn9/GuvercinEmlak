using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Dtos.UserDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IUserServices
{
    Task<ResponseDto<object>> Register(RegisterDto registerDto);
}