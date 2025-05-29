using Guvercin.Application.Dtos.AuthDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Helpers;
using Guvercin.Application.Services.Abstract;

namespace Guvercin.Application.Services.Concrete;

public class AuthService:IAuthServices
{
    private readonly TokenHelpers _tokenHelpers;

    public AuthService(TokenHelpers tokenHelpers)
    {
        _tokenHelpers = tokenHelpers;
    }

    public async Task<ResponseDto<object>> GenarateToken(TokenDto dto)
    {
        try
        {
            var checkUser=dto.Email=="gkayn@hotmail.com"?true:false;
            if (checkUser)
            {
                var token=_tokenHelpers.GenerateToken(dto);
                return new ResponseDto<object>() { Success = true, Data = token };
            }
            return new ResponseDto<object>(){ Success = false, Message = "User not found", Data = null, ErrorCode = ErrorCodes.Unauthorized  };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>
                { Success = false, Message = "Something went wrong", Data = null, ErrorCode = ErrorCodes.Exception };
        }

            }
}