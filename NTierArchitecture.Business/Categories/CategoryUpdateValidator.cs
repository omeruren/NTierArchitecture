using FluentValidation;
using NTierArchitecture.Entity.Dtos.Category;

namespace NTierArchitecture.Business.Categories;

public sealed class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Category Name is required.");
    }
}
