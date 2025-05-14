using FluentValidation;
using Guvercin.Application.Dtos.CategoryDtos;

namespace Guvercin.Application.Validators.Category;

public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(3, 30).WithMessage("Category name must be between 3 and 30 characters.");
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category id is required.");
    }
    
}