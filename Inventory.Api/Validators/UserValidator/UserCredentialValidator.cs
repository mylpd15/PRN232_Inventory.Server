using FluentValidation;
using Inventory.Domain;

namespace Inventory.Api;

public class UserCredentialValidator : AbstractValidator<UserCredentialDto>
{
    public UserCredentialValidator()
    {
        RuleFor(x => x.Username)
            .Length(5, 15);
        RuleFor(x => x.Password)
            .Length(6, 20);
    }
}
