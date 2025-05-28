using FluentValidation;
using Guvercin.Application.Dtos.AdvertItemsDtos;

namespace Guvercin.Application.Validators.AdvertItem;

public class UpdateAdvertItemValidator: AbstractValidator<UpdateAdvertItemDto>
{
    public UpdateAdvertItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Advert item name must not be empty")
            .Length(3, 50).WithMessage("Advert item name must be between 3 and 50 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Advert item price must not be empty")
            .GreaterThan(0).WithMessage("Advert item price must be greater than 0");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category id must not be empty");
    }
}