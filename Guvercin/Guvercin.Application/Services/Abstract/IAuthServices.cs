using Guvercin.Application.Dtos.AuthDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Dtos.UserDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IAuthServices
{
    Task<ResponseDto<object>> GenarateToken(LoginDto dto);
}