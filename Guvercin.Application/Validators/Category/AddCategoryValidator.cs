using FluentValidation;
using Guvercin.Application.Dtos.CategoryDtos;

namespace Guvercin.Application.Validators.Category;

public class AddCategoryValidator:AbstractValidator<CreateCategoryDto>
{
    public AddCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(3, 30).WithMessage("Category name must be between 3 and 30 characters.");
        
        
    }
    
}