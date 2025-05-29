using Guvercin.Application.Dtos.AuthDtos;
using Guvercin.Application.Dtos.ResponseDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IAuthServices
{
    Task<ResponseDto<object>> GenarateToken(TokenDto dto);
}