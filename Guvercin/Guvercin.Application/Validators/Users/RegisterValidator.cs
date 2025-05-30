using FluentValidation;
using Guvercin.Application.Dtos.UserDtos;

namespace Guvercin.Application.Validators.Users;

public class RegisterValidator:AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be between 2  characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MinimumLength(2).WithMessage("Surname must be between 2  characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
    
}