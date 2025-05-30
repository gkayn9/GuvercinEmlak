using AutoMapper;
using FluentValidation;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;

namespace Guvercin.Application.Services.Concrete;

public class UserService:IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterDto> _registerValidator;

    public UserService(IUserRepository userRepository, IValidator<RegisterDto> registerValidator)
    {
        _userRepository = userRepository;
        _registerValidator = registerValidator;
    }


    public async Task<ResponseDto<object>> Register(RegisterDto registerDto)
    {
        try
        {
            var validate= await _registerValidator.ValidateAsync(registerDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>(){
                    Success = false,
                    Data = null,
                    Message = validate.Errors.FirstOrDefault()?.ErrorMessage,
                    ErrorCode = ErrorCodes.ValidationError,
                };
            }
            var result = await _userRepository.RegisterAsync(registerDto);
            if (result.Succeeded)
            {
                return new ResponseDto<object>(){ Success = true, Data = null, Message = "User registered successfully" };
            }
            else
            {
                return new ResponseDto<object>() { Success = false, Data = null, Message = result.Errors.FirstOrDefault()?.Description };
            }
        }
        catch (Exception e)
        { 
            return new ResponseDto<object>(){Success = false,Data = null,Message = "Something went wrong",ErrorCode = ErrorCodes.Exception};
        }
    }
}