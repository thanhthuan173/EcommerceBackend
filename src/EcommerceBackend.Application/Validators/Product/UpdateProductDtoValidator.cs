using EcommerceBackend.Application.Dto.Product;
using FluentValidation;

namespace EcommerceBackend.Application.Validators.Product
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not over 100 characters.")
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not over 500 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Invalid price.")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid stock quantity.")
                .When(x => x.Stock.HasValue);

            RuleFor(x => x.ImgUrl)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid image URL.")
                .When(x => x.ImgUrl != null);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Invalid category.")
                .When(x => x.CategoryId.HasValue);
        }
    }
}