using AutoMapper;
using Guvercin.Application.Dtos.AuthDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Application.Helpers;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;

namespace Guvercin.Application.Services.Concrete;

public class AuthService:IAuthServices
{
    private readonly TokenHelpers _tokenHelpers;
    private readonly IUserRepository _userRepository;

    public AuthService(TokenHelpers tokenHelpers, IUserRepository userRepository)
    {
        _tokenHelpers = tokenHelpers;
        _userRepository = userRepository;
    }

    public async Task<ResponseDto<object>> GenarateToken(LoginDto dto)
    {
        try
        {
            //var checkUser=dto.Email=="gkayn@hotmail.com"?true:false;
            var checkUser= await _userRepository.CheckUser(dto.Email);
            
            if (checkUser.Id != null)
            {
                var user = await _userRepository.CheckUserWithPassword(dto);
                if (user.Succeeded)
                {
                    var tokenDto= new TokenDto
                    {
                        Email = dto.Email,
                        Id = checkUser.Id,
                        Role="Admin"
                        
                    };
                    var token=_tokenHelpers.GenerateToken(tokenDto);
                    return new ResponseDto<object>() { Success = true, Data = token };
                }
                return new ResponseDto<object>(){ Success = false, Message = "User not found", Data = null, ErrorCode = ErrorCodes.Unauthorized  };

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