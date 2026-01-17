using FluentValidation;
using NTierArchitecture.Entity.Dtos.Category;

namespace NTierArchitecture.Business.Categories;

public sealed class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Category Name is required.");
    }
}
