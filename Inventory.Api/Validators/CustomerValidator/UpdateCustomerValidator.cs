using FluentValidation;
using WareSync.Api.DTOs;
using WareSync.Business;

namespace WareSync.Api.Validators;
public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerValidator()
    {
    
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters")
            .Matches(@"^[a-zA-Z0-9\s\-_]+$").WithMessage("Customer name can only contain letters, numbers, spaces, hyphens, and underscores");

        RuleFor(x => x.CustomerAddress)
            .MaximumLength(200).WithMessage("Customer address cannot exceed 200 characters")
            .When(x => !string.IsNullOrEmpty(x.CustomerAddress));
    }
} 