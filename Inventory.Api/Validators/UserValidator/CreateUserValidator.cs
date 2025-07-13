using FluentValidation;
using Inventory.Domain;

namespace Inventory.Api;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password);
        RuleFor(x => new UserCredentialDto { Username = x.Username, Password = x.Password })
            .SetValidator(new UserCredentialValidator());
    }
}
