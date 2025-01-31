using System.Data;
using Backend.DTOs;
using FluentValidation;
namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name is required and must be less than 50 characters");
            RuleFor(b => b.Name).Length(2, 20).WithMessage("Long of name must be between 2 and 20 characters");
            RuleFor(b => b.BrandId).NotNull().WithMessage("BrandId is obligatory");
            RuleFor(b => b.BrandId).GreaterThan(0).WithMessage("Error that Brand value sent is not valid");
            RuleFor(b => b.Alcohol).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
