using EcommerceBackend.Application.Dto.Product;
using FluentValidation;

namespace EcommerceBackend.Application.Validators.Product
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not over 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not over 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Invalid price.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid stock quantity.");

            RuleFor(x => x.ImgUrl)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid image URL.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Invalid category.");
        }
    }
}