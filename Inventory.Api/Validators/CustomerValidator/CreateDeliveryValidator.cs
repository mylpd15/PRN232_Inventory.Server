using FluentValidation;
using WareSync.Business;

namespace WareSync.Api.Validators;
public class CreateDeliveryValidator : AbstractValidator<CreateDeliveryDto>
{
    public CreateDeliveryValidator()
    {
        RuleFor(x => x.SalesDate)
            .NotEmpty().WithMessage("Sales date is required")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Sales date cannot be in the future");

        RuleFor(x => x.CustomerID)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0");

        RuleFor(x => x.DeliveryDetails)
            .NotEmpty().WithMessage("At least one delivery detail is required")
            .Must(x => x.Count > 0).WithMessage("At least one delivery detail is required");

        RuleForEach(x => x.DeliveryDetails).SetValidator(new CreateDeliveryDetailValidator());
    }
}

public class CreateDeliveryDetailValidator : AbstractValidator<CreateDeliveryDetailDto>
{
    public CreateDeliveryDetailValidator()
    {
        RuleFor(x => x.ProductID)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0");

        RuleFor(x => x.DeliveryQuantity)
            .GreaterThan(0).WithMessage("Delivery quantity must be greater than 0")
            .LessThanOrEqualTo(10000).WithMessage("Delivery quantity cannot exceed 10,000");

        RuleFor(x => x.ExpectedDate)
            .NotEmpty().WithMessage("Expected date is required")
            .GreaterThan(DateTime.Now).WithMessage("Expected date must be in the future");
    }
} 