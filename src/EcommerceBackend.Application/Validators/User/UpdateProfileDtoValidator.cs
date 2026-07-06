using EcommerceBackend.Application.Dto.User;
using FluentValidation;

namespace EcommerceBackend.Application.Validators.User
{
    public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
{
    public UpdateProfileDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not be over 100 characters.")
            .When(x => x.FullName != null);

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
            .MaximumLength(50).WithMessage("Username must not be over 50 characters.")
            .When(x => x.Username != null);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .When(x => x.Email != null);

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(x => x == "Male" || x == "Female" || x == "Other")
            .WithMessage("Gender must be Male, Female, or Other.")
            .When(x => x.Gender != null);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(0|\+84)[0-9]{9,10}$")
            .WithMessage("Invalid phone number format.")
            .When(x => x.Phone != null);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(255).WithMessage("Address must not be over 255 characters.")
            .When(x => x.Address != null);
    }
}
}